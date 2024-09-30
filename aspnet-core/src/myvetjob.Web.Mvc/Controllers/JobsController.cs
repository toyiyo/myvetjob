using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
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

        public async Task<ActionResult> Index(GetActiveJobsInput input)
        {
            try
            {
                var jobs = await _jobAppService.GetActiveJobsAsync(input);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_JobPartial", jobs);
                }
                else
                {
                    return View(jobs);
                }
            }
            catch (ArgumentNullException) { return new NotFoundResult(); }
            catch (Abp.Domain.Entities.EntityNotFoundException) { return new NotFoundResult(); }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return View();
            }
        }

        public IActionResult SkillsMatch()
        {
            return View();
        }

        //set url to /jobs/{jobId}
        /// <summary>
        /// Displays the details of a job.
        /// </summary>
        /// <param name="jobId">The ID of the job.</param>
        /// <returns>The view displaying the job details.</returns>
        [Route("jobs/{jobId}")]
        public async Task<IActionResult> JobDetails(int jobId, string position, string companyName)
        {
            try
            {
                var job = await _jobAppService.GetActiveJobByIdAsync(jobId, position, companyName);
                return View(job);
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
