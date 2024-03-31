using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace myvetjob.Web.Views
{
    public abstract class myvetjobRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected myvetjobRazorPage()
        {
            LocalizationSourceName = myvetjobConsts.LocalizationSourceName;
        }
    }
}
