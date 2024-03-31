using Abp.AspNetCore.Mvc.ViewComponents;

namespace myvetjob.Web.Views
{
    public abstract class myvetjobViewComponent : AbpViewComponent
    {
        protected myvetjobViewComponent()
        {
            LocalizationSourceName = myvetjobConsts.LocalizationSourceName;
        }
    }
}
