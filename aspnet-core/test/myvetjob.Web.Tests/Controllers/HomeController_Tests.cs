using System.Threading.Tasks;
using myvetjob.Models.TokenAuth;
using myvetjob.Web.Controllers;
using Shouldly;
using Xunit;

namespace myvetjob.Web.Tests.Controllers
{
    public class HomeController_Tests: myvetjobWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}