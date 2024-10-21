using FirebirdResource.ApiService;

namespace FirebirdResource.Web;

public class CatalogApiClient(HttpClient httpClient)
{
    public async Task<CatalogBrand[]> GetBrandsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<CatalogBrand>? brands = null;

        await foreach (var brand in httpClient.GetFromJsonAsAsyncEnumerable<CatalogBrand>("/catalog/brands", cancellationToken))
        {
            if (brands?.Count >= maxItems)
            {
                break;
            }
            if (brand is not null)
            {
                brands ??= [];
                brands.Add(brand);
            }
        }

        return brands?.ToArray() ?? [];
    }
}

