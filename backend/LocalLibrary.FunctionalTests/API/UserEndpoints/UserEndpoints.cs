using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LocalLibrary.API.Endpoints.Users.DTOs;
using LocalLibrary.Core.Extensions;
using Xunit;

namespace LocalLibrary.FunctionalTests.API.UserEndpoints
{
    [Collection("Sequential")]
    public class UserEndpoints : IClassFixture<ApiTestFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public UserEndpoints(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsSuccessAndUsersListWithAdminUserToken()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var response = await Client.GetAsync("api/users");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<DetailUserDto[]>();

            Assert.Equal(2, model.Length);
        }

        [Fact]
        public async Task ReturnsForbiddenGivenNonAdminUser()
        {
            var userToken = ApiTokenHelper.GetNormalUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await Client.GetAsync("api/users");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsUnauthorizedGivenNoUser()
        {
            var response = await Client.GetAsync("api/users");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task AddsAUserReturnsOkWithAdminUser()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var request = new NewUserDto
            {
                Email = "new.user@email.com"
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");


            var response = await Client.PostAsync("api/users", jsonContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}