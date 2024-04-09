using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public interface IJobAppServicePublic {
        Task<JobDto> GetUnexpiredJobByIdAsync(int jobId);
        Task<PagedResultDto<JobDto>> GetUnexpiredJobsAsync();
    }
}
