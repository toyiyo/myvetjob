using Abp.Application.Services.Dto;

namespace myvetjob.Jobs
{
    public class GetAllJobsInput : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public bool IncludeExpiredJobs { get; set; }
        public GetAllJobsInput()
        {
            //default values, max result count is 100 to avoid overloading the DB
            MaxResultCount = 100;
        }
    }
}