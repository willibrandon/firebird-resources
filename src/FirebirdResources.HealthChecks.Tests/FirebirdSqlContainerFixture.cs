﻿using FirebirdResources.Testcontainers;

namespace FirebirdResources.HealthChecks.Tests;

public class FirebirdSqlContainerFixture : IAsyncLifetime
{
    private readonly FirebirdContainer _firebirdSqlContainer = new FirebirdBuilder()
        .Build();

    public FirebirdContainer FirebirdSql => _firebirdSqlContainer;

    public Task InitializeAsync() => _firebirdSqlContainer.StartAsync();

    public Task DisposeAsync() => _firebirdSqlContainer.DisposeAsync().AsTask();
}
