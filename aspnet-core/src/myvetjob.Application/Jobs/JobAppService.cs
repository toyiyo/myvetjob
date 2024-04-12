using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{

    public class JobAppService : myvetjobAppServiceBase, IJobAppService
    {
        private readonly IJobManager _jobManager;

        public JobAppService(IJobManager jobManager)
        {
            _jobManager = jobManager;
        }
        public async Task<JobDto> GetActiveJobByIdAsync(int jobId)
        {
            var job = await _jobManager.GetAsync(jobId);
            return ObjectMapper.Map<JobDto>(job);
        }
        /// <summary>
        /// Retrieves a list of active jobs asynchronously. That is, jobs that have been paid for and have not expired.
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<JobDto>> GetActiveJobsAsync(GetActiveJobsInput input)
        {
            var getAllJobsInput = new GetAllJobsInput
            {
                CompanyName = input.CompanyName,
                Position = input.Position,
                EmploymentType = input.EmploymentType,
                IncludeExpiredJobs = false,
                OrderStatus = OrderStatus.Paid,
                MinSalary = input.MinSalary,
                CreatedWithinDays = input.CreatedWithinDays,
                SkipCount = input.SkipCount,
                MaxResultCount = input.MaxResultCount,
                Sorting = input.Sorting,
            };

            var jobs = await _jobManager.GetAllAsync(getAllJobsInput);
            var totalJobs = await _jobManager.GetAllCountAsync(getAllJobsInput);
            return new PagedResultDto<JobDto>(totalJobs, ObjectMapper.Map<List<JobDto>>(jobs));
        }

    }
}