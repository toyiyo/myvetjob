using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public interface IJobAppService {
        Task<JobDto> GetActiveJobByIdAsync(string jobId, GetActiveJobsInput input);
        Task<PagedResultDto<JobDto>> GetActiveJobsAsync(GetActiveJobsInput input);
    }
}
