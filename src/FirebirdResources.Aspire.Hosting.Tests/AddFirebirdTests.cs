using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using Xunit;

namespace FirebirdResources.Aspire.Hosting.Tests;

public class AddFirebirdTests
{
    [Fact]
    public void AddFirebirdAddsGeneratedPasswordParameterWithUserSecretsParameterDefaultInRunMode()
    {
        var builder = DistributedApplication.CreateBuilder();

        var firebird = builder.AddFirebird("firebird");

        Assert.Equal("Aspire.Hosting.ApplicationModel.UserSecretsParameterDefault", firebird.Resource.PasswordParameter.Default?.GetType().FullName);
    }

    [Fact]
    public void AddFirebirdContainerWithDefaultsAddsAnnotationMetadata()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder.AddFirebird("firebird");

        using var app = appBuilder.Build();

        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var containerResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        Assert.Equal("firebird", containerResource.Name);

        var endpoint = Assert.Single(containerResource.Annotations.OfType<EndpointAnnotation>());
        Assert.Equal(3050, endpoint.TargetPort);
        Assert.False(endpoint.IsExternal);
        Assert.Equal("tcp", endpoint.Name);
        Assert.Null(endpoint.Port);
        Assert.Equal(ProtocolType.Tcp, endpoint.Protocol);
        Assert.Equal("tcp", endpoint.Transport);
        Assert.Equal("tcp", endpoint.UriScheme);

        var containerAnnotation = Assert.Single(containerResource.Annotations.OfType<ContainerImageAnnotation>());
        Assert.Equal(FirebirdContainerImageTags.Tag, containerAnnotation.Tag);
        Assert.Equal(FirebirdContainerImageTags.Image, containerAnnotation.Image);
        Assert.Equal(FirebirdContainerImageTags.Registry, containerAnnotation.Registry);
    }

    [Fact]
    public async Task FirebirdCreatesConnectionString()
    {
        var appBuilder = DistributedApplication.CreateBuilder();
        appBuilder.Configuration["Parameters:pass"] = "p@ssw0rd1";

        var pass = appBuilder.AddParameter("pass");
        appBuilder
            .AddFirebird("firebird", password: pass)
            .WithEndpoint("tcp", e => e.AllocatedEndpoint = new AllocatedEndpoint(e, "localhost", 3050))
            .AddDatabase("firebirdDb");

        using var app = appBuilder.Build();

        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var connectionStringResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        var connectionString = await connectionStringResource.GetConnectionStringAsync(default);

        Assert.Equal("Host=localhost;Port=3050;Username=SYSDBA;Password=p@ssw0rd1", connectionString);
        Assert.Equal("Host={firebird.bindings.tcp.host};Port={firebird.bindings.tcp.port};Username=SYSDBA;Password={pass.value}", connectionStringResource.ConnectionStringExpression.ValueExpression);
    }

    [Fact]
    public async Task FirebirdDatabaseCreatesConnectionString()
    {
        var appBuilder = DistributedApplication.CreateBuilder();
        appBuilder.Configuration["Parameters:pass"] = "p@ssw0rd1";

        var pass = appBuilder.AddParameter("pass");
        appBuilder
            .AddFirebird("firebird", password: pass)
            .WithEndpoint("tcp", e => e.AllocatedEndpoint = new AllocatedEndpoint(e, "localhost", 3050))
            .AddDatabase("firebirdDb");

        using var app = appBuilder.Build();

        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var sqlResource = Assert.Single(appModel.Resources.OfType<FirebirdDatabaseResource>());
        var connectionStringResource = sqlResource;
        var connectionString = await connectionStringResource.GetConnectionStringAsync();

        Assert.Equal("Host=localhost;Port=3050;Username=SYSDBA;Password=p@ssw0rd1;Database=/var/lib/firebird/data/firebirdDb", connectionString);
        Assert.Equal("{firebird.connectionString};Database=/var/lib/firebird/data/firebirdDb", connectionStringResource.ConnectionStringExpression.ValueExpression);
    }

    [Fact]
    public void ThrowsWithIdenticalChildResourceNames()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        var firebird = appBuilder.AddFirebird("firebird");
        firebird.AddDatabase("db");

        Assert.Throws<DistributedApplicationException>(() => firebird.AddDatabase("db"));
    }

    [Fact]
    public void ThrowsWithIdenticalChildResourceNamesDifferentParents()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder.AddFirebird("firebird1")
            .AddDatabase("db");

        var db = appBuilder.AddFirebird("firebird2");
        Assert.Throws<DistributedApplicationException>(() => db.AddDatabase("db"));
    }

    [Fact]
    public void CanAddDatabasesWithDifferentNamesOnSingleServer()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        var firebird = appBuilder.AddFirebird("firebird");

        var db1 = firebird.AddDatabase("db1", "customers1");
        var db2 = firebird.AddDatabase("db2", "customers2");

        Assert.Equal("customers1", db1.Resource.DatabaseName);
        Assert.Equal("customers2", db2.Resource.DatabaseName);

        Assert.Equal("{firebird.connectionString};Database=/var/lib/firebird/data/customers1", db1.Resource.ConnectionStringExpression.ValueExpression);
        Assert.Equal("{firebird.connectionString};Database=/var/lib/firebird/data/customers2", db2.Resource.ConnectionStringExpression.ValueExpression);
    }

    [Fact]
    public void CanAddDatabasesWithTheSameNameOnMultipleServers()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        var db1 = appBuilder.AddFirebird("firebird1")
            .AddDatabase("db1", "imports");

        var db2 = appBuilder.AddFirebird("firebird2")
            .AddDatabase("db2", "imports");

        Assert.Equal("imports", db1.Resource.DatabaseName);
        Assert.Equal("imports", db2.Resource.DatabaseName);

        Assert.Equal("{firebird1.connectionString};Database=/var/lib/firebird/data/imports", db1.Resource.ConnectionStringExpression.ValueExpression);
        Assert.Equal("{firebird2.connectionString};Database=/var/lib/firebird/data/imports", db2.Resource.ConnectionStringExpression.ValueExpression);
    }

    [Fact]
    public async Task WithPasswordAddsEnvironmentVariable()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder
            .AddFirebird("firebird")
            .WithPassword("p@ssw0rd1");

        using var app = appBuilder.Build();
        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var firebirdResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        var envVars = await firebirdResource.GetEnvironmentVariableValuesAsync();

        Assert.True(envVars.ContainsKey("FIREBIRD_PASSWORD"));
        Assert.Equal("p@ssw0rd1", envVars["FIREBIRD_PASSWORD"]);
    }

    [Fact]
    public async Task WithRootPasswordAddsEnvironmentVariable()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder
            .AddFirebird("firebird")
            .WithRootPassword("p@ssw0rd1");

        using var app = appBuilder.Build();
        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var firebirdResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        var envVars = await firebirdResource.GetEnvironmentVariableValuesAsync();

        Assert.True(envVars.ContainsKey("FIREBIRD_ROOT_PASSWORD"));
        Assert.Equal("p@ssw0rd1", envVars["FIREBIRD_ROOT_PASSWORD"]);
    }

    [Fact]
    public async Task WithTimeZoneAddsEnvironmentVariable()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder
            .AddFirebird("firebird")
            .WithTimeZone("America/Los_Angeles");

        using var app = appBuilder.Build();
        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var firebirdResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        var envVars = await firebirdResource.GetEnvironmentVariableValuesAsync();

        Assert.True(envVars.ContainsKey("TZ"));
        Assert.Equal("America/Los_Angeles", envVars["TZ"]);
    }

    [Fact]
    public async Task WithUseLegacyAuthAddsEnvironmentVariable()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder
            .AddFirebird("firebird")
            .WithUseLegacyAuth();

        using var app = appBuilder.Build();
        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var firebirdResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        var envVars = await firebirdResource.GetEnvironmentVariableValuesAsync();

        Assert.True(envVars.ContainsKey("FIREBIRD_USE_LEGACY_AUTH"));
        Assert.Equal(true.ToString(), envVars["FIREBIRD_USE_LEGACY_AUTH"]);
    }

    [Fact]
    public async Task WithUsernameAddsEnvironmentVariable()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        appBuilder
            .AddFirebird("firebird")
            .WithUsername("sally");

        using var app = appBuilder.Build();
        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var firebirdResource = Assert.Single(appModel.Resources.OfType<FirebirdServerResource>());
        var envVars = await firebirdResource.GetEnvironmentVariableValuesAsync();

        Assert.True(envVars.ContainsKey("FIREBIRD_USER"));
        Assert.Equal("sally", envVars["FIREBIRD_USER"]);
    }
}
