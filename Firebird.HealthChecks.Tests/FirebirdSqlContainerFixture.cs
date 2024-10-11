using DotNet.Testcontainers.Builders;
using Testcontainers.FirebirdSql;

namespace Firebird.HealthChecks.Tests;

public class FirebirdSqlContainerFixture : IAsyncLifetime
{
    private readonly FirebirdSqlContainer _firebirdSqlContainer = new FirebirdSqlBuilder()
        .WithWaitStrategy(Wait.ForUnixContainer().UntilContainerIsHealthy())
        .Build();

    public FirebirdSqlContainer FirebirdSql => _firebirdSqlContainer;

    public Task InitializeAsync() => _firebirdSqlContainer.StartAsync();

    public Task DisposeAsync() => _firebirdSqlContainer.DisposeAsync().AsTask();
}
