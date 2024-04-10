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
        public async Task<JobDto> GetUnexpiredJobByIdAsync(int jobId)
        {
            var job = await _jobManager.GetUnexpiredJobByIdAsync(jobId);
            return ObjectMapper.Map<JobDto>(job);
        }
        public async Task<PagedResultDto<JobDto>> GetUnexpiredJobsAsync()
        {
            var jobs = await _jobManager.GetUnexpiredJobsAsync();
            var totalJobs = await _jobManager.GetCountUnexpiredJobsAsync();
            return new PagedResultDto<JobDto>(totalJobs, ObjectMapper.Map<List<JobDto>>(jobs));
        }

    }
}