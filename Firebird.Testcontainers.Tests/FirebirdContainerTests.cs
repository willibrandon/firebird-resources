using FirebirdSql.Data.FirebirdClient;
using JetBrains.Annotations;
using System.Data;
using System.Data.Common;

namespace Firebird.Testcontainers.Tests;

public abstract class FirebirdContainerTests : IAsyncLifetime
{
    private readonly FirebirdContainer _firebirdContainer;

    private FirebirdContainerTests(FirebirdContainer firebirdSqlContainer) => _firebirdContainer = firebirdSqlContainer;

    public Task InitializeAsync() => _firebirdContainer.StartAsync();

    public Task DisposeAsync() => _firebirdContainer.DisposeAsync().AsTask();

    [Fact]
    public void ConnectionStateReturnsOpen()
    {
        // Given
        using DbConnection connection = new FbConnection(_firebirdContainer.GetConnectionString());

        // When
        connection.Open();

        // Then
        Assert.Equal(ConnectionState.Open, connection.State);
    }

    [Fact]
    public async Task ExecScriptReturnsSuccessful()
    {
        // Given
        const string scriptContent = "SELECT 1 FROM RDB$DATABASE;";

        // When
        var execResult = await _firebirdContainer.ExecScriptAsync(scriptContent)
            .ConfigureAwait(true);

        // Then
        Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
        Assert.Empty(execResult.Stderr);
    }

    [UsedImplicitly]
    public sealed class FirebirdSql3 : FirebirdContainerTests
    {
        public FirebirdSql3()
            : base(new FirebirdBuilder().WithImage("ghcr.io/fdcastel/firebird:3").Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdSql4: FirebirdContainerTests
    {
        public FirebirdSql4()
            : base(new FirebirdBuilder().WithImage("ghcr.io/fdcastel/firebird:4").Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdSql5 : FirebirdContainerTests
    {
        public FirebirdSql5()
            : base(new FirebirdBuilder().WithImage("ghcr.io/fdcastel/firebird:5").Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdSqlLatest : FirebirdContainerTests
    {
        public FirebirdSqlLatest()
            : base(new FirebirdBuilder().WithImage("ghcr.io/fdcastel/firebird:latest").Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdSqlSysdba : FirebirdContainerTests
    {
        public FirebirdSqlSysdba()
            : base(new FirebirdBuilder().WithUsername("SYSDBA").WithRootPassword("some-password").Build())
        {
        }
    }
}