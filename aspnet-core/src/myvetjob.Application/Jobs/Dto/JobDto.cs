using System;
using Abp.AutoMapper;

namespace myvetjob.Jobs
{
    [AutoMap(typeof(Job))]
    public class JobDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public string JobLocation { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string ApplyUrl { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}