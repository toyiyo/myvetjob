using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using myvetjob.Authorization;

namespace myvetjob
{
    [DependsOn(
        typeof(myvetjobCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class myvetjobApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<myvetjobAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(myvetjobApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
