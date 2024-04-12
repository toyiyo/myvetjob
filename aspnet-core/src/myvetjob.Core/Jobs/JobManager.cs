using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Timing;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Job> GetAsync(int jobId)
        {
            //Only return jobs that are paid for and have not expired
            return await _jobRepository.FirstOrDefaultAsync(j => j.Id == jobId
            && j.OrderStatus == OrderStatus.Paid
            && j.ExpireDate > Clock.Now);
        }

        /// <summary>
        /// Retrieves a list of jobs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of unexpired jobs.</returns>
        [UnitOfWork]
        public async Task<IReadOnlyList<Job>> GetAllAsync(GetAllJobsInput input)
        {
            return await GetJobsQuery(input)
            .OrderBy<Job>(input?.Sorting ?? "CreationTime DESC") //at some point we'll have ranking implemented
            .Skip(input?.SkipCount ?? 0)
            .Take(input?.MaxResultCount ?? int.MaxValue)
            .ToListAsync();
        }

        public Task<int> GetAllCountAsync(GetAllJobsInput input)
        {
            return GetJobsQuery(input).CountAsync();
        }


        private IQueryable<Job> GetJobsQuery(GetAllJobsInput input)
        {
            var query = _jobRepository.GetAll()
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), j => j.CompanyName.Contains(input.Keyword) || j.Position.Contains(input.Keyword))
            .WhereIf(input.EmploymentType.HasValue, j => j.EmploymentType == input.EmploymentType.Value)
            .WhereIf(input.OrderStatus.HasValue, j => j.OrderStatus == input.OrderStatus.Value)
            .WhereIf(!input.IncludeExpiredJobs, j => j.ExpireDate > Clock.Now);

            return query;
        }
    }
}