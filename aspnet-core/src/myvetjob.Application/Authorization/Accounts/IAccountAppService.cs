using System.Threading.Tasks;
using Abp.Application.Services;
using myvetjob.Authorization.Accounts.Dto;

namespace myvetjob.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
