using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LingEdu.Users.IntegrationTests;

public class HealthCheckTests : IClassFixture<WebApplicationFactory<LingEdu.WebApi.Program>>
{
    private readonly HttpClient _client;

    public HealthCheckTests(WebApplicationFactory<LingEdu.WebApi.Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_Returns_Ok()
    {
        var response = await _client.GetAsync("/api/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
