using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirebirdResources.Aspire.Client.Tests;

public class FirebirdClientExtensionsTests
{
    private const string ConnectionString = "Host=fake;Database=/var/lib/firebird/data/employees";

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ReadsFromConnectionStringsCorrectly(bool useKeyed)
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:firebirdDb", ConnectionString)
        ]);

        if (useKeyed)
        {
            builder.AddKeyedFirebirdClient("firebirdDb");
        }
        else
        {
            builder.AddFirebirdClient("firebirdDb");
        }

        using var host = builder.Build();
        var factory = useKeyed ?
            host.Services.GetRequiredKeyedService<FirebirdConnectionFactory>("firebirdDb") :
            host.Services.GetRequiredService<FirebirdConnectionFactory>();
        var connectionString = factory.Settings.ConnectionString;

        Assert.Equal(ConnectionString, connectionString);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConnectionStringCanBeSetInCode(bool useKeyed)
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:firebirdDb", "unused")
        ]);

        static void SetConnectionString(FirebirdClientSettings settings) => settings.ConnectionString = ConnectionString;
        if (useKeyed)
        {
            builder.AddKeyedFirebirdClient("firebirdDb", SetConnectionString);
        }
        else
        {
            builder.AddFirebirdClient("firebirdDb", SetConnectionString);
        }

        using var host = builder.Build();
        var factory = useKeyed ?
            host.Services.GetRequiredKeyedService<FirebirdConnectionFactory>("firebirdDb") :
            host.Services.GetRequiredService<FirebirdConnectionFactory>();
        var connectionString = factory.Settings.ConnectionString;

        Assert.Equal(ConnectionString, connectionString);
        // the connection string from config should not be used since code set it explicitly
        Assert.DoesNotContain("unused", connectionString);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConnectionNameWinsOverConfigSection(bool useKeyed)
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);

        var key = useKeyed ? "firebirdDb" : null;
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>(CreateConfigKey(FirebirdClientSettings.DefaultConfigSectionName, key, "ConnectionString"), "unused"),
            new KeyValuePair<string, string?>("ConnectionStrings:firebirdDb", ConnectionString)
        ]);

        if (useKeyed)
        {
            builder.AddKeyedFirebirdClient("firebirdDb");
        }
        else
        {
            builder.AddFirebirdClient("firebirdDb");
        }

        using var host = builder.Build();
        var factory = useKeyed ?
            host.Services.GetRequiredKeyedService<FirebirdConnectionFactory>("firebirdDb") :
            host.Services.GetRequiredService<FirebirdConnectionFactory>();

        Assert.Equal(ConnectionString, factory.Settings.ConnectionString);
        // the connection string from config should not be used since it was found in ConnectionStrings
        Assert.DoesNotContain("unused", factory.Settings.ConnectionString);
    }

    [Fact]
    public void CanAddMultipleKeyedServices()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:firebirdDb1", "Host=fake1;Database=/var/lib/firebird/data/employees"),
            new KeyValuePair<string, string?>("ConnectionStrings:firebirdDb2", "Host=fake2;Database=/var/lib/firebird/data/employees"),
            new KeyValuePair<string, string?>("ConnectionStrings:firebirdDb3", "Host=fake3;Database=/var/lib/firebird/data/employees"),
        ]);

        builder.AddFirebirdClient("firebirdDb1");
        builder.AddKeyedFirebirdClient("firebirdDb2");
        builder.AddKeyedFirebirdClient("firebirdDb3");

        using var host = builder.Build();

        var factory1 = host.Services.GetRequiredService<FirebirdConnectionFactory>();
        var factory2 = host.Services.GetRequiredKeyedService<FirebirdConnectionFactory>("firebirdDb2");
        var factory3 = host.Services.GetRequiredKeyedService<FirebirdConnectionFactory>("firebirdDb3");

        Assert.NotSame(factory1, factory2);
        Assert.NotSame(factory1, factory3);
        Assert.NotSame(factory2, factory3);

        Assert.Contains("fake1", factory1.Settings.ConnectionString);
        Assert.Contains("fake2", factory2.Settings.ConnectionString);
        Assert.Contains("fake3", factory3.Settings.ConnectionString);
    }

    private static string CreateConfigKey(string prefix, string? key, string suffix)
        => string.IsNullOrEmpty(key) ? $"{prefix}:{suffix}" : $"{prefix}:{key}:{suffix}";
}