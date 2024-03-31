using Microsoft.AspNetCore.Mvc;
using myvetjob.Controllers;

namespace myvetjob.Web.Controllers
{
    public class JobsController : myvetjobControllerBase
    {
        public ActionResult Index()
        {
            //get a list of jobs
            return View();
        }
	}
}
