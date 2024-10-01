using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using Abp.Domain.Entities;

namespace myvetjob.Jobs
{

    public class JobAppService : myvetjobAppServiceBase, IJobAppService
    {
        private readonly IJobManager _jobManager;
        private readonly IUsaJobManager _usaJobsManager;

        public JobAppService(IJobManager jobManager, IUsaJobManager usaJobsManager)
        {
            _jobManager = jobManager;
            _usaJobsManager = usaJobsManager;
        }
        public async Task<JobDto> GetActiveJobByIdAsync(string jobId, GetActiveJobsInput input)
        {
            Job localJob = null;
            Job usaJob = null;
            GetAllJobsInput getAllJobsInput = CreateGetAllJobsInput(input);

            if (int.TryParse(jobId, out int localJobId))
            {
                // Query the local job manager
                localJob = await _jobManager.GetAsync(localJobId).ConfigureAwait(false);
            }
            else
            {
                // Query the USA jobs manager
                usaJob = await _usaJobsManager.SearchAsync(jobId, getAllJobsInput).ConfigureAwait(false);
            }

            // Combine results, preferring local job if available
            var job = localJob ?? usaJob;

            if (job is null)
            {
                // Handle the case where no job is found
                throw new EntityNotFoundException($"A job with the ID {jobId} was not found in any source.");
            }

            return ObjectMapper.Map<JobDto>(job);
        }
        /// <summary>
        /// Retrieves a list of active jobs asynchronously. That is, jobs that have been paid for and have not expired.
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<JobDto>> GetActiveJobsAsync(GetActiveJobsInput input)
        {
            GetAllJobsInput getAllJobsInput = CreateGetAllJobsInput(input);

            var localJobs = await _jobManager.GetAllAsync(getAllJobsInput);
            var usaJobs = await _usaJobsManager.GetAllAsync(getAllJobsInput);
            var allJobs = localJobs.Concat(usaJobs.Items).ToList();
            var totalJobsCount = await _jobManager.GetAllCountAsync(getAllJobsInput) + usaJobs.TotalCount;
            return new PagedResultDto<JobDto>(totalJobsCount, ObjectMapper.Map<List<JobDto>>(allJobs));
        }

        private static GetAllJobsInput CreateGetAllJobsInput(GetActiveJobsInput input)
        {
            return new GetAllJobsInput
            {
                CompanyName = input.CompanyName,
                Position = input.Position,
                JobLocation = input.JobLocation,
                EmploymentType = input.EmploymentType,
                IncludeExpiredJobs = false,
                OrderStatus = OrderStatus.Paid,
                MinSalary = input.MinSalary,
                CreatedWithinDays = input.CreatedWithinDays,
                SkipCount = input.SkipCount,
                MaxResultCount = input.MaxResultCount,
                Sorting = input.Sorting,
            };
        }
    }
}