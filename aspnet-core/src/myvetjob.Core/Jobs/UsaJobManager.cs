using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Services;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using myvetjob.Authorization.Users;
using System.Web;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public class UsaJobManager : DomainService, IUsaJobManager
    {
        private readonly HttpClient _httpClient;
        private static readonly string USAJOBS_SEARCH_URL;
        private static readonly string USAJOBS_AUTH_KEY;
        private static readonly string USAJOBS_USER_AGENT;

        static UsaJobManager()
        {
            USAJOBS_AUTH_KEY = Environment.GetEnvironmentVariable("USAJOBS_AUTH_KEY");
            USAJOBS_USER_AGENT = Environment.GetEnvironmentVariable("USAJOBS_USER_AGENT");
            USAJOBS_SEARCH_URL = Environment.GetEnvironmentVariable("USAJOBS_SEARCH_URL");
        }

        public UsaJobManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Host", "data.usajobs.gov");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", USAJOBS_USER_AGENT);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization-Key", USAJOBS_AUTH_KEY);
        }

        /// <summary>
        /// Retrieves an unexpired job by its ID.
        /// </summary>
        /// <param name="jobId">The ID of the job to retrieve.</param>
        /// <returns>The unexpired job with the specified ID, or null if not found.</returns>
        public async Task<Job> SearchAsync(string jobId, GetAllJobsInput input)
        {
            var jobSearchResult = await GetAllAsync(input);
            var usaJob = jobSearchResult.Items.FirstOrDefault(x => x.ExternalId == jobId);
            //map the job to our Job entity
            return usaJob;
        }

        /// <summary>
        /// Retrieves a list of jobs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of unexpired jobs.</returns>
        public async Task<PagedResultDto<Job>> GetAllAsync(GetAllJobsInput input)
        {
            var url = BuildUrl(input);
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usaJobs = JsonConvert.DeserializeObject<Root>(content);

                // Convert the USAJobs data to your Job objects
                //todo: set id to the 
                var jobs = usaJobs.SearchResult.SearchResultItems.Select(usaJob =>
                {
                    Job job = MapUsaJobToJob(usaJob);
                    //need to refactor our model to include an external id and use that in the UI when available
                    //job.Id = int.Parse(usaJob.MatchedObjectDescriptor.PositionID);
                    return job;
                }
                ).ToList();

                var totalCount = usaJobs.SearchResult.SearchResultCountAll;
                var result = new PagedResultDto<Job>(totalCount, jobs);

                return result;
            }
            else
            {
                throw new Exception($"Failed to get jobs from USAJobs: {response.StatusCode}");
            }
        }

        private static Job MapUsaJobToJob(SearchResultItem usaJob)
        {
            var job = Job.Create(
                                user: new User { Id = 1 }, //hardcoded for now
                                companyName: usaJob.MatchedObjectDescriptor.OrganizationName,
                                position: usaJob.MatchedObjectDescriptor.PositionTitle,
                                description: usaJob.MatchedObjectDescriptor.UserArea.Details.GetFormattedDetails(),
                                employmentType: usaJob.MatchedObjectDescriptor.PositionSchedule.FirstOrDefault()?.ToEmploymentType() ?? EmploymentType.FullTime,
                                jobLocation: usaJob.MatchedObjectDescriptor.PositionLocationDisplay,
                                minSalary: Convert.ToDecimal(usaJob.MatchedObjectDescriptor.PositionRemuneration.FirstOrDefault()?.MinimumRange),
                                maxSalary: decimal.Parse(usaJob.MatchedObjectDescriptor.PositionRemuneration.FirstOrDefault()?.MaximumRange),
                                applyUrl: usaJob.MatchedObjectDescriptor.ApplyURI.FirstOrDefault(),
                                expireDays: usaJob.MatchedObjectDescriptor.ApplicationCloseDate.Subtract(DateTime.UtcNow.Date).Days,
                                ExternalId: usaJob.MatchedObjectDescriptor.PositionID
                            );
            job.CreationTime = usaJob.MatchedObjectDescriptor.PublicationStartDate;
            return job;
        }

        private static string BuildUrl(GetAllJobsInput input)
        {
            var builder = new UriBuilder(USAJOBS_SEARCH_URL);
            var query = HttpUtility.ParseQueryString(builder.Query);

            // Add query parameters based on the input
            if (!string.IsNullOrWhiteSpace(input.CompanyName))
                query["OrganizationName"] = input.CompanyName;
            if (!string.IsNullOrWhiteSpace(input.Position))
                query["PositionTitle"] = input.Position;
            if (!string.IsNullOrWhiteSpace(input.JobLocation) &&
                !ExcludedWords.Any(word => input.JobLocation.Contains(word, StringComparison.OrdinalIgnoreCase)))
                query["LocationName"] = input.JobLocation;
            if (input.MinSalary.HasValue)
                query["RemunerationMinimumAmount"] = input.MinSalary.ToString();
            if (input.CreatedWithinDays.HasValue && input.CreatedWithinDays.Value <= 60)
                query["DatePosted"] = input.CreatedWithinDays.ToString();
            if (input.MaxResultCount != 0)
                query["ResultsPerPage"] = input.MaxResultCount.ToString();
            if (input.SkipCount != 0)
                query["Page"] = ((input.SkipCount / input.MaxResultCount) + 1).ToString();

            builder.Query = query.ToString();
            return builder.ToString();
        }

        private static readonly HashSet<string> ExcludedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "multiple",
            "various",
            "varied",
            "several",
            "diverse",
            "numerous",
            "different",
            "assorted",
            "miscellaneous",
            "sundry",
            "throughout",
            "duty locations",
            "locations",
            "nationwide",
            "wide"
        };
    }
}