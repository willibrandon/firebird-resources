using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;

namespace Firebird.HealthChecks.Tests.Functional;

public class FirebirdHealthCheckTests(FirebirdSqlContainerFixture fixture) : IClassFixture<FirebirdSqlContainerFixture>
{
    private readonly FirebirdSqlContainerFixture _fixture = fixture;

    [Fact]
    public async Task Be_Healthy_If_Firebird_Is_Available()
    {
        var connectionString = _fixture.FirebirdSql.GetConnectionString();

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
    public async Task Be_Unhealthy_If_Firebird_Is_Not_Available()
    {
        var fbConnectionStringBuilder = new FbConnectionStringBuilder(_fixture.FirebirdSql.GetConnectionString())
        {
            Port = 1833
        };

        var webHostBuilder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHealthChecks()
                .AddFirebird(fbConnectionStringBuilder.ConnectionString, tags: ["firebird"]);
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
    public async Task Be_Unhealthy_If_SqlQuery_Spec_Is_Not_Valid()
    {
        var connectionString = _fixture.FirebirdSql.GetConnectionString();

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
