using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Timing;

namespace myvetjob.Jobs
{
    public class JobManager : DomainService, IJobManager
    {
        private readonly IRepository<Job> _jobRepository;
        public JobManager(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        /// <summary>
        /// Retrieves an unexpired job by its ID.
        /// </summary>
        /// <param name="jobId">The ID of the job to retrieve.</param>
        /// <returns>The unexpired job with the specified ID, or null if not found.</returns>
        public async Task<Job> GetUnexpiredJobByIdAsync(int jobId)
        {
            //get jobs that have not expired
            return await _jobRepository.FirstOrDefaultAsync(j => j.Id == jobId
            && j.OrderStatus == OrderStatus.Paid
            && j.ExpireDate > Clock.Now);
        }

        /// <summary>
        /// Retrieves a list of unexpired jobs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of unexpired jobs.</returns>
        [UnitOfWork]
        public async Task<List<Job>> GetUnexpiredJobsAsync()
        {
            //need to refactor to allow for pagination and filtering
            //get jobs that have not expired
            return await _jobRepository.GetAllListAsync(j => j.ExpireDate > Clock.Now
            && j.OrderStatus == OrderStatus.Paid);
        }

        public Task<int> GetCountUnexpiredJobsAsync()
        {
            return _jobRepository.CountAsync(j => j.ExpireDate > Clock.Now
            && j.OrderStatus == OrderStatus.Paid);
        }
    }
}