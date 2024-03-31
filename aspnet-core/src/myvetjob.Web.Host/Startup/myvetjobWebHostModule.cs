using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using myvetjob.Configuration;

namespace myvetjob.Web.Host.Startup
{
    [DependsOn(
       typeof(myvetjobWebCoreModule))]
    public class myvetjobWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public myvetjobWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myvetjobWebHostModule).GetAssembly());
        }
    }
}
