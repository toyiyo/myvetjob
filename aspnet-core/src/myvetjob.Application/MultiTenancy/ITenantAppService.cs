using Abp.Application.Services;
using myvetjob.MultiTenancy.Dto;

namespace myvetjob.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

