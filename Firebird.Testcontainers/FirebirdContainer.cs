using System.Text;

namespace Firebird.Testcontainers;

/// <inheritdoc cref="DockerContainer" />
[PublicAPI]
public sealed class FirebirdContainer : DockerContainer
{
    private readonly FirebirdConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdContainer" /> class.
    /// </summary>
    /// <param name="configuration">The container configuration.</param>
    public FirebirdContainer(FirebirdConfiguration configuration)
        : base(configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Gets the FirebirdSql connection string.
    /// </summary>
    /// <returns>The FirebirdSql connection string.</returns>
    public string GetConnectionString()
    {
        var properties = new Dictionary<string, string>
        {
            { "DataSource", Hostname },
            { "Port", GetMappedPublicPort(FirebirdBuilder.FirebirdSqlPort).ToString() },
            { "Database", Path.Combine(FirebirdBuilder.DefaultDatabaseLocation, _configuration.Database!) },
            { "User", _configuration.Username! },
            { "Password", _configuration.Password! }
        };

        return string.Join(";", properties.Select(property => string.Join("=", property.Key, property.Value)));
    }

    /// <summary>
    /// Gets the FirebirdSql SYSDBA connection string.
    /// </summary>
    /// <returns>The FirebirdSql connection string.</returns>
    public string GetSysDbaConnectionString()
    {
        var properties = new Dictionary<string, string>
        {
            { "DataSource", Hostname },
            { "Port", GetMappedPublicPort(FirebirdBuilder.FirebirdSqlPort).ToString() },
            { "Database", _configuration.Database! },
            { "User", FirebirdBuilder.SysDbaUsername },
            { "Password", _configuration.Password! }
        };

        return string.Join(";", properties.Select(property => string.Join("=", property.Key, property.Value)));
    }

    /// <summary>
    /// Executes the SQL script in the FirebirdSql container.
    /// </summary>
    /// <param name="scriptContent">The content of the SQL script to execute.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Task that completes when the SQL script has been executed.</returns>
    public async Task<ExecResult> ExecScriptAsync(string scriptContent, CancellationToken ct = default)
    {
        var scriptFilePath = string.Join("/", string.Empty, "tmp", Guid.NewGuid().ToString("D"), Path.GetRandomFileName());

        await CopyAsync(Encoding.Default.GetBytes(scriptContent), scriptFilePath, Unix.FileMode644, ct)
            .ConfigureAwait(false);

        return await ExecAsync(["isql", "-i", scriptFilePath, "-user", _configuration.Username, "-pass", _configuration.Password,
            Path.Combine(FirebirdBuilder.DefaultDatabaseLocation, _configuration.Database!)], ct)
                .ConfigureAwait(false);
    }
}