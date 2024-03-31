using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using myvetjob.EntityFrameworkCore;
using myvetjob.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace myvetjob.Web.Tests
{
    [DependsOn(
        typeof(myvetjobWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class myvetjobWebTestModule : AbpModule
    {
        public myvetjobWebTestModule(myvetjobEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(myvetjobWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(myvetjobWebMvcModule).Assembly);
        }
    }
}