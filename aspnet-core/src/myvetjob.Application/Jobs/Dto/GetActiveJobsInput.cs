using Abp.Application.Services.Dto;
namespace myvetjob.Jobs
{
    public class GetActiveJobsInput : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public EmploymentType? EmploymentType { get; set; }
    }
}