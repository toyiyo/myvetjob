using System.Collections.Generic;
using System.Threading.Tasks;

namespace myvetjob.Jobs
{
    public interface IUsaJobManager
    {
        Task<int> GetAllCountAsync(GetAllJobsInput input);
        Task<Job> GetAsync(int jobId);
        Task<IReadOnlyList<Job>> GetAllAsync(GetAllJobsInput input);
    }
}