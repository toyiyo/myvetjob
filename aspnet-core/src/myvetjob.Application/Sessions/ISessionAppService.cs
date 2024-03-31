using System.Threading.Tasks;
using Abp.Application.Services;
using myvetjob.Sessions.Dto;

namespace myvetjob.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
