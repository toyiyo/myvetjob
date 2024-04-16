using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;

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
            //we should update getAllAsync to include the total count so we avoid making two DB calls
            var localJobs = await _jobManager.GetAllAsync(getAllJobsInput);
            var usaJobs = await _usaJobsManager.GetAllAsync(getAllJobsInput);
            var allJobs = localJobs.Concat(usaJobs).ToList();
            var totalJobs = await _jobManager.GetAllCountAsync(getAllJobsInput) + usaJobs.Count(); //we can avoid the extra call since our UI doesn't need the total count
            return new PagedResultDto<JobDto>(totalJobs, ObjectMapper.Map<List<JobDto>>(allJobs));
        }

    }
}