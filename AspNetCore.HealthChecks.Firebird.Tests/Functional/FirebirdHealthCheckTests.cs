using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using Testcontainers.FirebirdSql;

namespace AspNetCore.HealthChecks.Firebird.Tests.Functional;

public class FirebirdHealthCheckTests : IAsyncLifetime
{
    private readonly FirebirdSqlContainer _firebirdSqlContainer = new FirebirdSqlBuilder()
        .WithWaitStrategy(Wait.ForUnixContainer())
        .Build();

    public Task InitializeAsync() => _firebirdSqlContainer.StartAsync();

    public Task DisposeAsync() => _firebirdSqlContainer.DisposeAsync().AsTask();

    [Fact]
    public async Task be_healthy_if_firebird_is_available()
    {
        //var connectionString = "Server=tcp:localhost,5433;Initial Catalog=master;User Id=sa;Password=Password12!;Encrypt=false";

        var connectionString = _firebirdSqlContainer.GetConnectionString();

        var webHostBuilder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHealthChecks()
                .AddFirebird(connectionString, tags: ["firebird"]);
            })
            .Configure(app =>
            {
                app.UseHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("firebird")
                });
            });

        using var server = new TestServer(webHostBuilder);

        using var response = await server.CreateRequest("/health").GetAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task be_unhealthy_if_firebird_is_not_available()
    {
        var webHostBuilder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHealthChecks()
                .AddFirebird("Server=tcp:localhost,1833;Initial Catalog=master;User Id=sa;Password=Password12!;Encrypt=false;Connection Timeout=10", tags: ["firebird"]);
            })
            .Configure(app =>
            {
                app.UseHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("firebird")
                });
            });

        using var server = new TestServer(webHostBuilder);

        using var response = await server.CreateRequest("/health").GetAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.ServiceUnavailable);
    }

    [Fact]
    public async Task be_unhealthy_if_sqlquery_spec_is_not_valid()
    {
        var connectionString = "Server=tcp:localhost,5433;Initial Catalog=master;User Id=sa;Password=Password12!;Encrypt=false";

        var webHostBuilder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHealthChecks()
                .AddFirebird(connectionString, healthQuery: "SELECT 1 FROM [NOT_VALID_DB]", tags: ["firebird"]);
            })
            .Configure(app =>
            {
                app.UseHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("firebird")
                });
            });

        using var server = new TestServer(webHostBuilder);

        using var response = await server.CreateRequest("/health").GetAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.ServiceUnavailable);
    }
}
