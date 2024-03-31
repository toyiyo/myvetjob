using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using myvetjob.Configuration.Dto;

namespace myvetjob.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : myvetjobAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
