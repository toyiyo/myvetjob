using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace myvetjob.Controllers
{
    public abstract class myvetjobControllerBase: AbpController
    {
        protected myvetjobControllerBase()
        {
            LocalizationSourceName = myvetjobConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
