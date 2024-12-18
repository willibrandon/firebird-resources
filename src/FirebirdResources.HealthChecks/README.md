﻿# FirebirdResources.HealthChecks

[![.NET](https://github.com/willibrandon/firebird-resources/actions/workflows/ci.yml/badge.svg)](https://github.com/willibrandon/firebird-resources/actions/workflows/ci.yml)

FirebirdResources.HealthChecks is an ASP.NET Core health check package for Firebird SQL databases.

```PowerShell
Install-Package FirebirdResources.HealthChecks -IncludePrerelease
```

Once the package is installed you can add the HealthCheck using the **AddFirebird** `IHealthChecksBuilder` extension methods.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHealthChecks()
        .AddFirebird(Configuration["Data:ConnectionStrings:Sql"]);
}
```

Firebird HealthCheck registration also supports healthQuery, name, failure status, tags, and other optional parameters.

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