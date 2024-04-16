using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Timing;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using myvetjob.Authorization.Users;
using System.Web;

namespace myvetjob.Jobs
{
    public class UsaJobManager : DomainService, IUsaJobManager
    {
        private readonly HttpClient _httpClient;

        public UsaJobManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Host", "data.usajobs.gov");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "jdelgado@toyiyo.com");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization-Key", "Z1SflWU3jcBa/NH0x7vcEwNQ7LOrpVq+IuXY4gtRTFw=");
        }

        /// <summary>
        /// Retrieves an unexpired job by its ID.
        /// </summary>
        /// <param name="jobId">The ID of the job to retrieve.</param>
        /// <returns>The unexpired job with the specified ID, or null if not found.</returns>
        public async Task<Job> GetAsync(int jobId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://data.usajobs.gov/api/search?JobID={jobId}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching job: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var jobSearchResult = JsonConvert.DeserializeObject<Root>(content);

            var usaJob = jobSearchResult.SearchResult.SearchResultItems.FirstOrDefault()?.MatchedObjectDescriptor;
            //map the job to our Job entity
            var job = Job.Create(
                user: new User { Id = 1 }, //hardcoded for now
                companyName: usaJob.OrganizationName,
                position: usaJob.PositionTitle,
                description: usaJob.UserArea.Details.GetFormattedDetails(),
                employmentType: usaJob.PositionSchedule.FirstOrDefault()?.ToEmploymentType() ?? EmploymentType.FullTime,
                jobLocation: usaJob.PositionLocationDisplay,
                minSalary: Convert.ToDecimal(usaJob.PositionRemuneration.FirstOrDefault()?.MinimumRange),
                maxSalary: decimal.Parse(usaJob.PositionRemuneration.FirstOrDefault()?.MaximumRange),
                applyUrl: usaJob.ApplyURI.FirstOrDefault()
            );

            return job;
        }

        /// <summary>
        /// Retrieves a list of jobs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of unexpired jobs.</returns>
        public async Task<IReadOnlyList<Job>> GetAllAsync(GetAllJobsInput input)
        {
            var url = BuildUrl(input);
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usaJobs = JsonConvert.DeserializeObject<Root>(content);

                // Convert the USAJobs data to your Job objects
                var jobs = usaJobs.SearchResult.SearchResultItems.Select(usaJob => Job.Create(
                user: new User { Id = 1 }, //hardcoded for now
                companyName: usaJob.MatchedObjectDescriptor.OrganizationName,
                position: usaJob.MatchedObjectDescriptor.PositionTitle,
                description: usaJob.MatchedObjectDescriptor.UserArea.Details.GetFormattedDetails(),
                employmentType: usaJob.MatchedObjectDescriptor.PositionSchedule.FirstOrDefault()?.ToEmploymentType() ?? EmploymentType.FullTime,
                jobLocation: usaJob.MatchedObjectDescriptor.PositionLocationDisplay,
                minSalary: Convert.ToDecimal(usaJob.MatchedObjectDescriptor.PositionRemuneration.FirstOrDefault()?.MinimumRange),
                maxSalary: decimal.Parse(usaJob.MatchedObjectDescriptor.PositionRemuneration.FirstOrDefault()?.MaximumRange),
                applyUrl: usaJob.MatchedObjectDescriptor.ApplyURI.FirstOrDefault()
            )).ToList();

                return jobs;
            }
            else
            {
                throw new Exception($"Failed to get jobs from USAJobs: {response.StatusCode}");
            }
        }

        private string BuildUrl(GetAllJobsInput input)
        {
            var builder = new UriBuilder("https://data.usajobs.gov/api/search");
            var query = HttpUtility.ParseQueryString(builder.Query);

            // Add query parameters based on the input
            if (!string.IsNullOrWhiteSpace(input.CompanyName))
                query["OrganizationName"] = input.CompanyName;
            if (!string.IsNullOrWhiteSpace(input.Position))
                query["PositionTitle"] = input.Position;
            //todo: filter by location - expand on remote as boolean rather than string, min salary, recency, and employment type
            //pagination Page=3&ResultsPerPage=50 using input skip and max result count

            builder.Query = query.ToString();
            return builder.ToString();
        }

        public Task<int> GetAllCountAsync(GetAllJobsInput input)
        {
            throw new NotImplementedException();
        }
    }
}