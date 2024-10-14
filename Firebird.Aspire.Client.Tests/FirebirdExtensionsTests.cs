using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Firebird.Aspire.Client.Tests;

public class FirebirdExtensionsTests
{
    private const string ConnectionString = "Host=fake;Database=/var/lib/firebird/data/employees";

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ReadsFromConnectionStringsCorrectly(bool useKeyed)
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:fbconnectionFactory", ConnectionString)
        ]);

        if (useKeyed)
        {
            builder.AddKeyedFirebirdClient("fbconnectionFactory");
        }
        else
        {
            builder.AddFirebirdClient("fbconnectionFactory");
        }

        using var host = builder.Build();
        var factory = useKeyed ?
            host.Services.GetRequiredKeyedService<FbConnectionFactory>("fbconnectionFactory") :
            host.Services.GetRequiredService<FbConnectionFactory>();
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
            new KeyValuePair<string, string?>("ConnectionStrings:fbconnectionFactory", "unused")
        ]);

        static void SetConnectionString(FirebirdSettings settings) => settings.ConnectionString = ConnectionString;
        if (useKeyed)
        {
            builder.AddKeyedFirebirdClient("fbconnectionFactory", SetConnectionString);
        }
        else
        {
            builder.AddFirebirdClient("fbconnectionFactory", SetConnectionString);
        }

        using var host = builder.Build();
        var factory = useKeyed ?
            host.Services.GetRequiredKeyedService<FbConnectionFactory>("fbconnectionFactory") :
            host.Services.GetRequiredService<FbConnectionFactory>();
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

        var key = useKeyed ? "fbconnectionFactory" : null;
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>(CreateConfigKey(FirebirdSettings.DefaultConfigSectionName, key, "ConnectionString"), "unused"),
            new KeyValuePair<string, string?>("ConnectionStrings:fbconnectionFactory", ConnectionString)
        ]);

        if (useKeyed)
        {
            builder.AddKeyedFirebirdClient("fbconnectionFactory");
        }
        else
        {
            builder.AddFirebirdClient("fbconnectionFactory");
        }

        using var host = builder.Build();
        var factory = useKeyed ?
            host.Services.GetRequiredKeyedService<FbConnectionFactory>("fbconnectionFactory") :
            host.Services.GetRequiredService<FbConnectionFactory>();

        Assert.Equal(ConnectionString, factory.Settings.ConnectionString);
        // the connection string from config should not be used since it was found in ConnectionStrings
        Assert.DoesNotContain("unused", factory.Settings.ConnectionString);
    }

    [Fact]
    public void CanAddMultipleKeyedServices()
    {
        var builder = Host.CreateEmptyApplicationBuilder(null);
        builder.Configuration.AddInMemoryCollection([
            new KeyValuePair<string, string?>("ConnectionStrings:fbconnectionFactory1", "Host=fake1;Database=/var/lib/firebird/data/employees"),
            new KeyValuePair<string, string?>("ConnectionStrings:fbconnectionFactory2", "Host=fake2;Database=/var/lib/firebird/data/employees"),
            new KeyValuePair<string, string?>("ConnectionStrings:fbconnectionFactory3", "Host=fake3;Database=/var/lib/firebird/data/employees"),
        ]);

        builder.AddFirebirdClient("fbconnectionFactory1");
        builder.AddKeyedFirebirdClient("fbconnectionFactory2");
        builder.AddKeyedFirebirdClient("fbconnectionFactory3");

        using var host = builder.Build();

        var factory1 = host.Services.GetRequiredService<FbConnectionFactory>();
        var factory2 = host.Services.GetRequiredKeyedService<FbConnectionFactory>("fbconnectionFactory2");
        var factory3 = host.Services.GetRequiredKeyedService<FbConnectionFactory>("fbconnectionFactory3");

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