using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using myvetjob.Controllers;

namespace myvetjob.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : myvetjobControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
