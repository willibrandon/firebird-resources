using Microsoft.EntityFrameworkCore;

namespace FirebirdResource.ApiService;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbContextOptions<CatalogDbContext> Options { get; } = options;

    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
}