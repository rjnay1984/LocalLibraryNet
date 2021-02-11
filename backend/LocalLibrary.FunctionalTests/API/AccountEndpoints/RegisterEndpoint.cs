using LocalLibrary.API.Endpoints.Account;
using LocalLibrary.Core.Constants;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace LocalLibrary.FunctionalTests.API.AccountEndpoints
{
    [Collection("Sequential")]
    public class RegisterEndpoint : IClassFixture<ApiTestFixture>
    {
        public RegisterEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Theory]
        [InlineData("gooduser@microsoft.com", AuthorizationConstants.DEFAULT_PASSWORD, HttpStatusCode.OK)]
        [InlineData("baduser@microsoft.com", "badpassword", HttpStatusCode.BadRequest)]
        public async Task ReturnsExpectedResultGivenCredentials(string testEmail, string testPassword, HttpStatusCode statusCode)
        {
            var request = new RegisterDto()
            {
                Email = testEmail,
                Password = testPassword
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/register", jsonContent);

            Assert.Equal(statusCode, response.StatusCode);
        }
    }
}
