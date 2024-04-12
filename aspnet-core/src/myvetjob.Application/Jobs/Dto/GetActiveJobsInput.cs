using Abp.Application.Services.Dto;
namespace myvetjob.Jobs
{
    public class GetActiveJobsInput : PagedAndSortedResultRequestDto
    {
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public decimal? MinSalary { get; set; }
        public int? CreatedWithinDays { get; set; }

    }
}