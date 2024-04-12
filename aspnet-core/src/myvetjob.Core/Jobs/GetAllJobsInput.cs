using Abp.Application.Services.Dto;
using Abp.Webhooks;

namespace myvetjob.Jobs
{
    public class GetAllJobsInput : PagedAndSortedResultRequestDto
    {
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public decimal? MinSalary { get; set; }
        public int? CreatedWithinDays { get; set; }
        public bool IncludeExpiredJobs { get; set; }

        public GetAllJobsInput()
        {
            //default values, max result count is 100 to avoid overloading the DB
            MaxResultCount = 100;
        }
    }
}