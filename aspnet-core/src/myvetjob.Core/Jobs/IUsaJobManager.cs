using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public interface IUsaJobManager
    {
        Task<Job> SearchAsync(string jobId, GetAllJobsInput input);
        Task<PagedResultDto<Job>> GetAllAsync(GetAllJobsInput input);
    }
}