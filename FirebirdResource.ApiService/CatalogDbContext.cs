using Microsoft.EntityFrameworkCore;

namespace FirebirdResource.ApiService;

public interface ICatalogDbContext
{
    public DbSet<CatalogBrand> CatalogBrands { get; }
}

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options), ICatalogDbContext
{
    public DbContextOptions<CatalogDbContext> Options { get; } = options;

    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
}