﻿namespace FirebirdResource.Tests;

public class HealthCheckTests : IClassFixture<DistributedApplicationFixture>
{
    private readonly DistributedApplicationFixture _fixture;
    private readonly HttpClient _httpClient;

    public HealthCheckTests(DistributedApplicationFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.App.CreateHttpClient("apiService");
    }

    [Fact]
    public async Task GetHealthReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
