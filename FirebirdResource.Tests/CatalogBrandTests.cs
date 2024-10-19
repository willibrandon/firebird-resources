using FirebirdResource.ApiService;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FirebirdResource.Tests;

public class CatalogBrandTests : IClassFixture<DistributedApplicationFixture>
{
    private readonly DistributedApplicationFixture _fixture;
    private readonly HttpClient _httpClient;

    public CatalogBrandTests(DistributedApplicationFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.App.CreateHttpClient("apiService");
    }

    [Fact]
    public async Task GetCatalogBrandsReturnsOkStatusCode()
    {
        using HttpResponseMessage response = await _httpClient.GetAsync("/catalogbrands");
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCatalogBrandByIdReturnsOkStatusCode()
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new CatalogBrand
            {
                Brand = "Test"
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage postResponse = await _httpClient.PostAsync("/catalogbrands", jsonContent);
        postResponse.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var postBrand = await postResponse.Content.ReadFromJsonAsync<CatalogBrand>();
        Console.WriteLine($"{postBrand}\n");

        using HttpResponseMessage getResponse = await _httpClient.GetAsync("/catalogbrands/" + postBrand?.Id);
        getResponse.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var getBrand = await getResponse.Content.ReadFromJsonAsync<CatalogBrand>();
        Console.WriteLine($"{getBrand}\n");

        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(postBrand);
        Assert.NotNull(getBrand);
        Assert.Equal(postBrand.Id, getBrand.Id);
        Assert.Equal(postBrand.Brand, getBrand.Brand);
    }

    [Fact]
    public async Task PostCatalogBrandsReturnsCreatedStatusCode()
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new CatalogBrand
            {
                Brand = "Test"
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await _httpClient.PostAsync("/catalogbrands", jsonContent);
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostPutCatalogBrandsReturnsCreatedOKStatusCodes()
    {
        using StringContent jsonContentPost = new(
            JsonSerializer.Serialize(new CatalogBrand
            {
                Brand = "Test1"
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage postResponse = await _httpClient.PostAsync("/catalogbrands", jsonContentPost);
        postResponse.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var brand = await postResponse.Content.ReadFromJsonAsync<CatalogBrand>();
        Console.WriteLine($"{brand}\n");

        using StringContent jsonContentPut = new(
            JsonSerializer.Serialize(new CatalogBrand
            {
                Brand = "Test2"
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage putResponse = await _httpClient.PutAsync($"/catalogbrands/{brand?.Id}", jsonContentPut);
        putResponse.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        brand = await putResponse.Content.ReadFromJsonAsync<CatalogBrand>();

        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        Assert.NotNull(brand);
        Assert.Equal("Test2", brand.Brand);
    }

    [Fact]
    public async Task PostDeleteCatalogBrandsReturnsCreatedOKStatusCodes()
    {
        using StringContent jsonContentPost = new(
            JsonSerializer.Serialize(new CatalogBrand
            {
                Brand = "Test1"
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage postResponse = await _httpClient.PostAsync("/catalogbrands", jsonContentPost);
        postResponse.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var brand = await postResponse.Content.ReadFromJsonAsync<CatalogBrand>();
        Console.WriteLine($"{brand}\n");

        using HttpResponseMessage deleteResponse = await _httpClient.DeleteAsync($"/catalogbrands/{brand?.Id}");
        deleteResponse.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await deleteResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        Assert.NotNull(brand);
        Assert.Contains($"{brand.Id} deleted", jsonResponse);
    }
}
