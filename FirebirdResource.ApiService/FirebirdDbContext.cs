using Microsoft.EntityFrameworkCore;

namespace FirebirdResource.ApiService;

public interface IFirebirdDbContext
{
    public DbSet<CatalogBrand> CatalogBrands { get; }
}

public class FirebirdtDbContext(DbContextOptions<FirebirdtDbContext> options) : DbContext(options), IFirebirdDbContext
{
    public DbContextOptions<FirebirdtDbContext> Options { get; } = options;

    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
}