using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI.Inputs;
using Microsoft.EntityFrameworkCore;
using myvetjob.Authorization.Users;

namespace myvetjob.Jobs
{
    [Index(nameof(CompanyName), nameof(Position), nameof(JobLocation), nameof(OrderStatus))]
    public class Job : FullAuditedEntity
    {
        public const int MaxPositionLength = 500; //todo: max length should be defined in the configuration
        public const int MaxDescriptionLength = 2000000; // 2MB limit | 307692 - 400000 words | 1230.8 - 1600.0 pages
        public const int DefaultExpireDays = 30;
        [Required]
        public string CompanyName { get; protected set; }
        /// <summary>
        /// Please specify a single position like "Software Developer" or "Project Manager". not a sentence.
        /// </summary>
        [Required]
        [StringLength(MaxPositionLength)]
        public string Position { get; protected set; }
        /// <summary>
        /// Job opening description, include all the details about the job.
        /// </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; protected set; }
        /// <summary>
        /// Type of employment like Full-time, Part-time, Contract, Internship, Volunteer, Temporary
        /// </summary>
        [Required]
        public EmploymentType EmploymentType { get; protected set; }
        /// <summary>
        /// Location of the job like "New York, NY" or "Remote"
        /// </summary>
        [Required]
        [StringLength(MaxPositionLength)]
        public string JobLocation { get; protected set; }
        /// <summary>
        /// Minimum salary for the position.  In USD.
        /// </summary>
        [Required]
        public decimal MinSalary { get; protected set; }
        /// <summary>
        /// Maximum salary for the position.  In USD.
        /// </summary>
        [Required]
        public decimal MaxSalary { get; protected set; }
        /// <summary>
        /// URL to apply for the job.
        /// </summary>
        [Required]
        public string ApplyUrl { get; protected set; }

        [Required]
        public OrderStatus OrderStatus { get; protected set; }
        public int ApplicationCount { get; protected set; }
        public DateTime ExpireDate { get; protected set; }
        /// <summary>
        /// We don't make constructor public and forcing to create events using <see cref="Create"/> method.
        /// But constructor can not be private since it's used by EntityFramework.
        /// Thats why we did it protected.
        /// </summary>
        protected Job()
        {
        }

        public static Job Create(User user, string companyName, string position, string description, EmploymentType employmentType, string jobLocation, decimal minSalary, decimal maxSalary, string applyUrl)
        {
            var job = new Job
            {
                CreatorUserId = user.Id,
                LastModifierUserId = user.Id,
                CreationTime = Clock.Now,
                LastModificationTime = Clock.Now,
                CompanyName = companyName,
                Position = position,
                Description = description,
                EmploymentType = employmentType,
                JobLocation = jobLocation,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                ApplyUrl = applyUrl,
                OrderStatus = OrderStatus.Pending,
                ApplicationCount = 0,
                ExpireDate = Clock.Now.AddDays(DefaultExpireDays)
            };

            return job;
        }

        private static void SetLastModified(Job job, User user)
        {
            job.LastModificationTime = Clock.Now;
            job.LastModifierUserId = user.Id;
        }
    }

}