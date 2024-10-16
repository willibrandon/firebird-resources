namespace FirebirdResource.Tests;

public class ApiServiceTransactionInfoTests : IClassFixture<DistributedApplicationFixture>
{
    private readonly DistributedApplicationFixture _fixture;
    private readonly HttpClient _httpClient;

    public ApiServiceTransactionInfoTests(DistributedApplicationFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.App.CreateHttpClient("apiService");
    }

    [Fact]
    public async Task GetTransactionSnapshotNumberReturnsOkStatusCode()
    {
        var response = await _httpClient.GetAsync("/fbtransactioninfo/gettransactionsnapshotnumber");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
