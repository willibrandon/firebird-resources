# Firebird.Testcontainers

Firebird.Testcontainers is a fluent testcontainer builder for the official Firebird Docker images.

Firebird.Testcontainers uses [Testcontainers for .NET](https://dotnet.testcontainers.org/) to spinup a docker container directly from the C# (unit test) code. This options requires docker service running locally.

```PowerShell
Install-Package Firebird.Testcontainers
```

## Build and Start

To build a container and startup a Firebird container:

```csharp

var firebirdSqlContainer = new FirebirdBuilder()
    .WithWaitStrategy(Wait.ForUnixContainer().UntilContainerIsHealthy())
    .Build();

await firebirdSqlContainer.StartAsync().ConfigureAwait(false);

```

## Methods

Todo.
