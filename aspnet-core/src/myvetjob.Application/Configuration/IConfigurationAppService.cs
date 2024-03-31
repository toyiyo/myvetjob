using System.Threading.Tasks;
using myvetjob.Configuration.Dto;

namespace myvetjob.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
