# Firebird.Testcontainers

Firebird.Testcontainers is a fluent testcontainer builder for Firebird Docker images.

Firebird.Testcontainers uses [Testcontainers for .NET](https://dotnet.testcontainers.org/) to spinup a docker container directly from the C# (unit test) code. This requires a docker service running locally.

## Installation

```PowerShell
Install-Package Firebird.Testcontainers
```

## Build and Start

To build and startup a Firebird container:

```csharp

var firebirdContainer = new FirebirdBuilder()
    .Build();

await firebirdContainer.StartAsync().ConfigureAwait(false);

string connectionString = firebirdContainer.GetConnectionString();

```

## Methods
