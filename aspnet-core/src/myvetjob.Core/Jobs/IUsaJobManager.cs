using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public interface IUsaJobManager
    {
        Task<Job> GetAsync(int jobId);
        Task<PagedResultDto<Job>> GetAllAsync(GetAllJobsInput input);
    }
}