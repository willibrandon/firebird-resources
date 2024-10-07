# AspNetCore.HealthChecks.Firebird

AspNetCore.HealthChecks.Firebird is a health check package for Firebird SQL databases.

```PowerShell
Install-Package AspNetCore.HealthChecks.Firebird
```

Once the package is installed you can add the HealthCheck using the **AddFirebird** `IHealthChecksBuilder` extension methods.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHealthChecks()
        .AddFirebird(Configuration["Data:ConnectionStrings:Sql"]);
}
```

Firebird HealthCheck registration also supports name, tags, failure status and other optional parameters.

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