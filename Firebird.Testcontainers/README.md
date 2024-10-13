# Firebird.Testcontainers

Firebird.Testcontainers is a fluent testcontainer builder for Firebird Docker images.

Firebird.Testcontainers uses [Testcontainers for .NET](https://dotnet.testcontainers.org/) to spinup a docker container directly from the C# (unit test) code. This package requires a docker service running locally.

## Installation

```PowerShell
Install-Package Firebird.Testcontainers
```

## Usage
### Build and Start

To build and startup a Firebird container:

```csharp

var firebirdContainer = new FirebirdBuilder()
    .Build();

await firebirdContainer.StartAsync().ConfigureAwait(false);

string connectionString = firebirdContainer.GetConnectionString();

```

### Methods
The following builder methods are available for the `FirebirdBuilder`:

| Method |  Example | What |
| -      | -        | -    |
| `WithConfig` | `.WithConfig("ConnectionTimeout", "90")` | Sets values in the Firebird configuration file (firebird.conf).
| `WithDatabase` | `.WithDatabase("employess")` | Sets the Firebird database name.
| `WithPassword` | `.WithPassword("yourStrong(!)Password")` | Sets the Firebird user password.
| `WithRootPassword` | `.WithRootPassword("masterkey")` | Sets the Firebird SYSDBA password.
| `WithTimeZone` | `.WithTimeZone("America/Los_Angeles")` | Sets the Firebird container time zone. e.g. "America/Los_Angeles".
| `WithUseLegacyAuth` | `.WithUseLegacyAuth()` | Enables legacy Firebird authentication (not recommended).
| `WithUsername` | `.WithUsername("user")` | Sets the Firebird user name.

The following container methods are available for the `FirebirdContainer`:

| Method |  Example | What |
| -      | -        | -    |
| `GetConnectionString` | `.GetConnectionString()` | Gets the Firebird connection string.
| `GetSysDbaConnectionString` | `.GetSysDbaConnectionString()` | Gets the Firebird SYSDBA connection string.
| `ExecScriptAsync` | `.ExecScriptAsync("SELECT 1 FROM RDB$DATABASE;")` | Executes the SQL script in the Firebird container.
| `ExecScriptAsSysDbaAsync` | `.ExecScriptAsSysDbaAsync("SELECT 1 FROM RDB$DATABASE;")` | Executes the SQL script in the Firebird container using the SYSDBA account.