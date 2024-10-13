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
        using DbConnection connection = new FbConnection(_firebirdContainer.GetConnectionString());

        connection.Open();

        Assert.Equal(ConnectionState.Open, connection.State);
    }

    [Fact]
    public async Task ExecScriptReturnsSuccessful()
    {
        const string scriptContent = "SELECT 1 FROM RDB$DATABASE;";

        var execResult = await _firebirdContainer.ExecScriptAsync(scriptContent)
            .ConfigureAwait(true);

        Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
        Assert.Empty(execResult.Stderr);
    }

    [Fact]
    public async Task ExecScriptAsSysDbaReturnsSuccessful()
    {
        const string scriptContent = "SELECT 1 FROM RDB$DATABASE;";

        var execResult = await _firebirdContainer.ExecScriptAsSysDbaAsync(scriptContent)
            .ConfigureAwait(true);

        Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
        Assert.Empty(execResult.Stderr);
    }

    [Fact]
    public void SysDbaConnectionStateReturnsOpen()
    {
        using DbConnection connection = new FbConnection(_firebirdContainer.GetSysDbaConnectionString());

        connection.Open();

        Assert.Equal(ConnectionState.Open, connection.State);
    }

    [UsedImplicitly]
    public sealed class Firebird : FirebirdContainerTests
    {
        public Firebird()
            : base(new FirebirdBuilder()
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class Firebird3 : FirebirdContainerTests
    {
        public Firebird3()
            : base(new FirebirdBuilder()
                  .WithImage("ghcr.io/fdcastel/firebird:3")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class Firebird4 : FirebirdContainerTests
    {
        public Firebird4()
            : base(new FirebirdBuilder()
                  .WithImage("ghcr.io/fdcastel/firebird:4")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class Firebird5 : FirebirdContainerTests
    {
        public Firebird5()
            : base(new FirebirdBuilder()
                  .WithImage("ghcr.io/fdcastel/firebird:5")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdLatest : FirebirdContainerTests
    {
        public FirebirdLatest()
            : base(new FirebirdBuilder()
                  .WithImage("ghcr.io/fdcastel/firebird:latest")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdWithConfig : FirebirdContainerTests
    {
        public FirebirdWithConfig()
            : base(new FirebirdBuilder()
                  .WithConfig("ConnectionTimeout", "90")
                  .WithConfig("DeadlockTimeout", "5")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdWithDatabase : FirebirdContainerTests
    {
        public FirebirdWithDatabase()
            : base(new FirebirdBuilder()
                  .WithDatabase("foo")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdWithTimeZone : FirebirdContainerTests
    {
        public FirebirdWithTimeZone()
            : base(new FirebirdBuilder()
                  .WithTimeZone("America/Los_Angeles")
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdWithUseLegacyAuth : FirebirdContainerTests
    {
        public FirebirdWithUseLegacyAuth()
            : base(new FirebirdBuilder()
                  .WithUseLegacyAuth()
                  .Build())
        {
        }
    }

    [UsedImplicitly]
    public sealed class FirebirdWithUsernameWithPassword : FirebirdContainerTests
    {
        public FirebirdWithUsernameWithPassword()
            : base(new FirebirdBuilder()
                  .WithUsername("foo")
                  .WithPassword("bar")
                  .Build())
        {
        }
    }
}