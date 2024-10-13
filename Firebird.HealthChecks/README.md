# Firebird.HealthChecks

[![.NET](https://github.com/willibrandon/firebird-resources/actions/workflows/dotnet.yml/badge.svg)](https://github.com/willibrandon/firebird-resources/actions/workflows/dotnet.yml)

Firebird.HealthChecks is an ASP.NET Core health check package for Firebird SQL databases.

```PowerShell
Install-Package Firebird.HealthChecks
```

Once the package is installed you can add the HealthCheck using the **AddFirebird** `IHealthChecksBuilder` extension methods.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHealthChecks()
        .AddFirebird(Configuration["Data:ConnectionStrings:Sql"]);
}
```

Firebird HealthCheck registration also supports healthQuery, name, tags, failure status, and other optional parameters.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddHealthChecks()
        .AddFirebird(
            connectionString: Configuration["Data:ConnectionStrings:Sql"],
            healthQuery: "SELECT 1 FROM RDB$DATABASE;",
            name: "sql",
            failureStatus: HealthStatus.Degraded,
            tags: new string[] { "db", "sql", "firebird" });
}
```