using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myvetjob.Controllers;
using myvetjob.Jobs;

namespace myvetjob.Web.Controllers
{
    public class JobsController : myvetjobControllerBase
    {
        private JobAppService _jobAppService;

        public JobsController(JobAppService jobAppService)
        {
            _jobAppService = jobAppService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var jobs = await _jobAppService.GetUnexpiredJobsAsync();
                return View(jobs);
            }
            catch (ArgumentNullException) { return new NotFoundResult(); }
            catch (Abp.Domain.Entities.EntityNotFoundException) { return new NotFoundResult(); }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return View();
            }
        }
	}
}
