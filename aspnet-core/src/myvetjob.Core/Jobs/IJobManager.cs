using System.Collections.Generic;
using System.Threading.Tasks;

namespace myvetjob.Jobs
{
    public interface IJobManager
    {
        Task<int> GetCountUnexpiredJobsAsync();
        Task<Job> GetUnexpiredJobByIdAsync(int jobId);
        Task<List<Job>> GetUnexpiredJobsAsync();
    }
}