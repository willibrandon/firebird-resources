using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Shouldly;

namespace AspNetCore.HealthChecks.Firebird.Tests.DependencyInjection;

public class RegistrationTests
{
    [Fact]
    public void add_health_check_when_properly_configured()
    {
        var services = new ServiceCollection()
            .AddHealthChecks()
            .AddFirebird("connectionstring")
            .Services;

        using var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();

        var registration = options.Value.Registrations.First();
        var check = registration.Factory(serviceProvider);

        registration.Name.ShouldBe("firebird");
        check.ShouldBeOfType<FirebirdHealthCheck>();
    }

    [Fact]
    public async Task invoke_beforeOpen_when_defined()
    {
        var services = new ServiceCollection();
        bool invoked = false;
        const string connectionstring = "Server=(local);Database=foo;User Id=bar;Password=baz;Connection Timeout=1";
        void beforeOpen(FbConnection connection)
        {
            invoked = true;
            connection.ConnectionString.ShouldBe(connectionstring);
        }
        services.AddHealthChecks()
            .AddFirebird(connectionstring, configure: beforeOpen);

        using var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();

        var registration = options.Value.Registrations.First();
        var check = registration.Factory(serviceProvider);

        await Record.ExceptionAsync(() => check.CheckHealthAsync(new HealthCheckContext()));
        invoked.ShouldBeTrue();
    }

    [Fact]
    public void add_named_health_check_when_properly_configured()
    {
        var services = new ServiceCollection()
            .AddHealthChecks()
            .AddFirebird("connectionstring", name: "my-firebird-1")
            .Services;

        using var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();

        var registration = options.Value.Registrations.First();
        var check = registration.Factory(serviceProvider);

        registration.Name.ShouldBe("my-firebird-1");
        check.ShouldBeOfType<FirebirdHealthCheck>();
    }

    [Fact]
    public void add_health_check_with_connection_string_factory_when_properly_configured()
    {
        var services = new ServiceCollection();
        bool factoryCalled = false;
        services.AddHealthChecks()
            .AddFirebird(_ =>
            {
                factoryCalled = true;
                return "connectionstring";
            });

        using var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();

        var registration = options.Value.Registrations.First();
        var check = registration.Factory(serviceProvider);

        registration.Name.ShouldBe("firebird");
        check.ShouldBeOfType<FirebirdHealthCheck>();
        factoryCalled.ShouldBeTrue();
    }
}
