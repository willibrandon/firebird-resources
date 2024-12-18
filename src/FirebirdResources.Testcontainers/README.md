﻿# FirebirdResources.Testcontainers

[![.NET](https://github.com/willibrandon/firebird-resources/actions/workflows/ci.yml/badge.svg)](https://github.com/willibrandon/firebird-resources/actions/workflows/ci.yml)

FirebirdResources.Testcontainers is a fluent testcontainer builder for Firebird SQL Docker images.

FirebirdResources.Testcontainers uses [Testcontainers for .NET](https://dotnet.testcontainers.org/) to spinup a docker container directly from C# (unit test) code. This package requires a docker service running locally.

## Installation

```PowerShell
Install-Package Firebird.Testcontainers -IncludePrerelease
```

## Usage
### Build and Start

To build and startup a Firebird container:

```csharp
var firebirdContainer = new FirebirdBuilder()
    .Build();

await firebirdContainer.StartAsync().ConfigureAwait(false);
```
### Get Connection Strings

To get connection strings from a Firebird container:

```csharp
string connectionString = firebirdContainer.GetConnectionString();
string sysDbaConnectionString = firebirdContainer.GetSysDbaConnectionString();
```

### Execute SQL Scripts

To execute SQL scripts on a Firebird container:

```csharp
firebirdContainer.ExecScriptAsync("SELECT 1 FROM RDB$DATABASE;");
firebirdContainer.ExecScriptAsSysDbaAsync("SELECT 1 FROM RDB$DATABASE;");
```

### Methods
The following builder methods are available for a `FirebirdBuilder`:

| Method |  Example | What |
| -      | -        | -    |
| `WithConfig` | `.WithConfig("ConnectionTimeout", "90")` | Sets values in the Firebird configuration file (firebird.conf).
| `WithDatabase` | `.WithDatabase("employees")` | Sets the Firebird database name.
| `WithPassword` | `.WithPassword("yourStrong(!)Password")` | Sets the Firebird user password.
| `WithRootPassword` | `.WithRootPassword("masterkey")` | Sets the Firebird SYSDBA password.
| `WithTimeZone` | `.WithTimeZone("America/Los_Angeles")` | Sets the Firebird container time zone. e.g. "America/Los_Angeles".
| `WithUseLegacyAuth` | `.WithUseLegacyAuth()` | Enables legacy Firebird authentication (not recommended).
| `WithUsername` | `.WithUsername("user")` | Sets the Firebird user name.

The following container methods are available for a `FirebirdContainer`:

| Method |  Example | What |
| -      | -        | -    |
| `GetConnectionString` | `.GetConnectionString()` | Gets the Firebird connection string.
| `GetSysDbaConnectionString` | `.GetSysDbaConnectionString()` | Gets the Firebird SYSDBA connection string.
| `ExecScriptAsync` | `.ExecScriptAsync("SELECT 1 FROM RDB$DATABASE;")` | Executes the SQL script in the Firebird container.
| `ExecScriptAsSysDbaAsync` | `.ExecScriptAsSysDbaAsync("SELECT 1 FROM RDB$DATABASE;")` | Executes the SQL script in the Firebird container using the SYSDBA account.