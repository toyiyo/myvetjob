using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public interface IJobAppService {
        Task<JobDto> GetActiveJobByIdAsync(int jobId);
        Task<PagedResultDto<JobDto>> GetActiveJobsAsync(GetActiveJobsInput input);
    }
}
