using Microsoft.EntityFrameworkCore;

namespace FirebirdResources.Aspire.EntityFrameworkCore.Client.Tests;

public interface ITestDbContext
{
    public DbSet<TestDbContext.CatalogBrand> CatalogBrands { get; }
}

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options), ITestDbContext
{
    public DbContextOptions<TestDbContext> Options { get; } = options;

    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();

    public class CatalogBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; } = default!;
    }
}