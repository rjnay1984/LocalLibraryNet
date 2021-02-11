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
    public class AuthenticateEndpoint : IClassFixture<ApiTestFixture>
    {
        public AuthenticateEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Theory]
        [InlineData("demouser@microsoft.com", AuthorizationConstants.DEFAULT_PASSWORD, HttpStatusCode.OK)]
        [InlineData("demouser@microsoft.com", "badpassword", HttpStatusCode.Unauthorized)]
        [InlineData("baduser@microsoft.com", "badpassword", HttpStatusCode.Unauthorized)]
        public async Task ReturnsExpectedResultGivenCredentials(string testUsername, string testPassword, HttpStatusCode statusCode)
        {
            var request = new LoginDto()
            {
                Username = testUsername,
                Password = testPassword
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/authenticate", jsonContent);

            Assert.Equal(statusCode, response.StatusCode);
        }
    }
}
