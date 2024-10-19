using FirebirdSql.EntityFrameworkCore.Firebird.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirebirdResources.Aspire.EntityFrameworkCore.Client.Tests;

public class FirebirdEFCoreExtensionsTests
{
    private const string ConnectionString = "Data Source=fake;Database=master;Encrypt=True";

    [Fact]
    public void ReadsFromConnectionStringsCorrectly()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString)
        ]);

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection");

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

        Assert.Equal(ConnectionString, context.Database.GetDbConnection().ConnectionString);
    }

    [Fact]
    public void ConnectionStringCanBeSetInCode()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", "unused")
        ]);

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection", settings => settings.ConnectionString = ConnectionString);

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

        var actualConnectionString = context.Database.GetDbConnection().ConnectionString;
        Assert.Equal(ConnectionString, actualConnectionString);
        // the connection string from config should not be used since code set it explicitly
        Assert.DoesNotContain("unused", actualConnectionString);
    }

    [Fact]
    public void ConnectionNameWinsOverConfigSection()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("Firebird:Aspire:EntityFrameworkCore:Client:ConnectionString", "unused"),
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString)
        ]);

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection");

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

        var actualConnectionString = context.Database.GetDbConnection().ConnectionString;
        Assert.Equal(ConnectionString, actualConnectionString);
        // the connection string from config should not be used since it was found in ConnectionStrings
        Assert.DoesNotContain("unused", actualConnectionString);
    }

    [Fact]
    public void CanConfigureDbContextOptions()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString),
            new KeyValuePair<string, string?>("Firebird:Aspire:EntityFrameworkCore:Client:DisableRetry", "false"),
            new KeyValuePair<string, string?>("Firebird:Aspire:EntityFrameworkCore:Client:CommandTimeout", "608")
        ]);

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection", configureDbContextOptions: optionsBuilder =>
        {
            optionsBuilder.UseFirebird(ConnectionString, sqlBuilder =>
            {
                sqlBuilder.MinBatchSize(123);
            });
        });

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

#pragma warning disable EF1001 // Internal EF Core API usage.

        var extension = context.Options.FindExtension<FbOptionsExtension>();
        Assert.NotNull(extension);

        // ensure the min batch size was respected
        Assert.Equal(123, extension.MinBatchSize);

        // ensure the connection string from config was respected
        var actualConnectionString = context.Database.GetDbConnection().ConnectionString;
        Assert.Equal(ConnectionString, actualConnectionString);

        // ensure the retry strategy is enabled and set to its default value
        Assert.NotNull(extension.ExecutionStrategyFactory);
        var executionStrategy = extension.ExecutionStrategyFactory(new ExecutionStrategyDependencies(new CurrentDbContext(context), context.Options, null!));
        var retryStrategy = Assert.IsType<FirebirdRetryingExecutionStrategy>(executionStrategy);
        Assert.Equal(new WorkaroundToReadProtectedField(context).MaxRetryCount, retryStrategy.MaxRetryCount);

        // ensure the command timeout from config was respected
        Assert.Equal(608, extension.CommandTimeout);

#pragma warning restore EF1001 // Internal EF Core API usage.
    }

    [Fact]
    public void CanConfigureDbContextOptionsWithoutRetry()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString),
            new KeyValuePair<string, string?>("Firebird:Aspire:EntityFrameworkCore:Client:DisableRetry", "true"),
        ]);

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection", configureDbContextOptions: optionsBuilder =>
        {
            optionsBuilder.UseFirebird(ConnectionString, sqlBuilder =>
            {
                sqlBuilder.CommandTimeout(123);
            });
        });

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

#pragma warning disable EF1001 // Internal EF Core API usage.

        var extension = context.Options.FindExtension<FbOptionsExtension>();
        Assert.NotNull(extension);

        // ensure the command timeout was respected
        Assert.Equal(123, extension.CommandTimeout);

        // ensure the connection string from config was respected
        var actualConnectionString = context.Database.GetDbConnection().ConnectionString;
        Assert.Equal(ConnectionString, actualConnectionString);

        // ensure no retry strategy was registered
        Assert.Null(extension.ExecutionStrategyFactory);

#pragma warning restore EF1001 // Internal EF Core API usage.
    }

    [Theory]
    [InlineData(true)]
    //[InlineData(false)]
    public void CanConfigureCommandTimeout(bool useSettings)
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString)
        ]);

        if (!useSettings)
        {
            builder.Configuration.AddInMemoryCollection([
                new KeyValuePair<string, string?>("Firebird:Aspire:EntityFrameworkCore:Client:CommandTimeout", "608")
            ]);
        }

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection",
                configureDbContextOptions: optionsBuilder => optionsBuilder.UseFirebird(ConnectionString),
                configureSettings: useSettings ? settings => settings.CommandTimeout = 608 : null);

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

#pragma warning disable EF1001 // Internal EF Core API usage.

        var extension = context.Options.FindExtension<FbOptionsExtension>();
        Assert.NotNull(extension);

        // ensure the command timeout was respected
        Assert.Equal(608, extension.CommandTimeout);

#pragma warning restore EF1001 // Internal EF Core API usage.
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CommandTimeoutFromBuilderWinsOverOthers(bool useSettings)
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString)
        ]);
        if (!useSettings)
        {
            builder.Configuration.AddInMemoryCollection([
                new KeyValuePair<string, string?>("Firebird:Aspire:EntityFrameworkCore:Client:CommandTimeout", "400")
            ]);
        }

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection",
                configureDbContextOptions: optionsBuilder =>
                    optionsBuilder.UseFirebird(ConnectionString, builder => builder.CommandTimeout(123)),
                configureSettings: useSettings ? settings => settings.CommandTimeout = 300 : null);

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();

#pragma warning disable EF1001 // Internal EF Core API usage.

        var extension = context.Options.FindExtension<FbOptionsExtension>();
        Assert.NotNull(extension);

        // ensure the command timeout from builder was respected
        Assert.Equal(123, extension.CommandTimeout);

#pragma warning restore EF1001 // Internal EF Core API usage.
    }

    /// <summary>
    /// Verifies that two different DbContexts can be registered with different connection strings.
    /// </summary>
    [Fact]
    public void CanHave2DbContexts()
    {
        const string connectionString2 = "Data Source=fake2;Database=master2;Encrypt=True";

        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString),
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection2", connectionString2),
        ]);

        builder.AddFirebirdDbContext<TestDbContext>("sqlconnection");
        builder.AddFirebirdDbContext<TestDbContext2>("sqlconnection2");

        using var host = builder.Build();
        var context = host.Services.GetRequiredService<TestDbContext>();
        var context2 = host.Services.GetRequiredService<TestDbContext2>();

        var actualConnectionString = context.Database.GetDbConnection().ConnectionString;
        Assert.Equal(ConnectionString, actualConnectionString);

        actualConnectionString = context2.Database.GetDbConnection().ConnectionString;
        Assert.Equal(connectionString2, actualConnectionString);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ThrowsWhenDbContextIsRegisteredBeforeAspireComponent(bool useServiceType)
    {
        var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings { EnvironmentName = Environments.Development });
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString)
        ]);

        if (useServiceType)
        {
            builder.Services.AddDbContextPool<ITestDbContext, TestDbContext>(options => options.UseFirebird(ConnectionString));
        }
        else
        {
            builder.Services.AddDbContextPool<TestDbContext>(options => options.UseFirebird(ConnectionString));
        }

        var exception = Assert.Throws<InvalidOperationException>(() => builder.AddFirebirdDbContext<TestDbContext>("sqlconnection"));
        Assert.Equal("DbContext<TestDbContext> is already registered. Please ensure 'services.AddDbContext<TestDbContext>()' is not used when calling 'AddFirebirdDbContext()' or use the corresponding 'Enrich' method.", exception.Message);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DoesntThrowWhenDbContextIsRegisteredBeforeAspireComponentProduction(bool useServiceType)
    {
        var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings { EnvironmentName = Environments.Production });
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:sqlconnection", ConnectionString)
        ]);

        if (useServiceType)
        {
            builder.Services.AddDbContextPool<ITestDbContext, TestDbContext>(options => options.UseFirebird(ConnectionString));
        }
        else
        {
            builder.Services.AddDbContextPool<TestDbContext>(options => options.UseFirebird(ConnectionString));
        }

        var exception = Record.Exception(() => builder.AddFirebirdDbContext<TestDbContext>("sqlconnection"));

        Assert.Null(exception);
    }

    public class TestDbContext2(DbContextOptions<TestDbContext2> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = default!;
        }
    }
}