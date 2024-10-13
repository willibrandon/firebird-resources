using System.Text;

namespace Firebird.Testcontainers;

/// <inheritdoc cref="DockerContainer" />
/// <summary>
/// Initializes a new instance of the <see cref="FirebirdContainer" /> class.
/// </summary>
/// <param name="configuration">The container configuration.</param>
[PublicAPI]
public sealed class FirebirdContainer(FirebirdConfiguration configuration) : DockerContainer(configuration)
{
    private readonly FirebirdConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    /// <summary>
    /// Gets the Firebird connection string.
    /// </summary>
    /// <returns>The Firebird connection string.</returns>
    public string GetConnectionString()
    {
        if (_configuration.Username == null)
        {
            throw new InvalidOperationException(nameof(_configuration.Username) + "is not set.");
        }

        if (_configuration.Password == null)
        {
            throw new InvalidOperationException(nameof(_configuration.Password) + "is not set.");
        }

        string database = _configuration.Database != null
            ? Path.Combine(FirebirdBuilder.DefaultDatabaseLocation, _configuration.Database)
            : FirebirdBuilder.DefaultDatabase; ;

        var properties = new Dictionary<string, string>
        {
            { "DataSource", Hostname },
            { "Port", GetMappedPublicPort(FirebirdBuilder.Port).ToString() },
            { "Database", database },
            { "User", _configuration.Username },
            { "Password", _configuration.Password }
        };

        return string.Join(";", properties.Select(property => string.Join("=", property.Key, property.Value)));
    }

    /// <summary>
    /// Gets the Firebird SYSDBA connection string.
    /// </summary>
    /// <returns>The Firebird SYSDBA connection string.</returns>
    public string GetSysDbaConnectionString()
    {
        string database = _configuration.Database != null
            ? Path.Combine(FirebirdBuilder.DefaultDatabaseLocation, _configuration.Database)
            : FirebirdBuilder.DefaultDatabase; ;

        var properties = new Dictionary<string, string>
        {
            { "DataSource", Hostname },
            { "Port", GetMappedPublicPort(FirebirdBuilder.Port).ToString() },
            { "Database", database },
            { "User", FirebirdBuilder.SysDbaUsername },
            { "Password", FirebirdBuilder.SysDbaPassword }
        };

        return string.Join(";", properties.Select(property => string.Join("=", property.Key, property.Value)));
    }

    /// <summary>
    /// Executes the SQL script in the Firebird container.
    /// </summary>
    /// <param name="scriptContent">The content of the SQL script to execute.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Task that completes when the SQL script has been executed.</returns>
    public async Task<ExecResult> ExecScriptAsync(string scriptContent, CancellationToken ct = default)
        => await ExecScriptAsync(_configuration.Username!, _configuration.Password!, scriptContent, ct)
            .ConfigureAwait(false);

    /// <summary>
    /// Executes the SQL script in the Firebird container using the SYSDBA account.
    /// </summary>
    /// <param name="scriptContent">The content of the SQL script to execute.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Task that completes when the SQL script has been executed.</returns>
    public async Task<ExecResult> ExecScriptAsSysDbaAsync(string scriptContent, CancellationToken ct = default)
        => await ExecScriptAsync(FirebirdBuilder.SysDbaUsername, FirebirdBuilder.SysDbaPassword, scriptContent, ct)
            .ConfigureAwait(false);

    private async Task<ExecResult> ExecScriptAsync(string username, string password, string scriptContent, CancellationToken ct = default)
    {
        var scriptFilePath = string.Join("/", string.Empty, "tmp", Guid.NewGuid().ToString("D"), Path.GetRandomFileName());

        await CopyAsync(Encoding.Default.GetBytes(scriptContent), scriptFilePath, Unix.FileMode644, ct)
            .ConfigureAwait(false);

        return await ExecAsync([
            "isql", "-i", scriptFilePath,
            "-user", username,
            "-pass", password,
            Path.Combine(FirebirdBuilder.DefaultDatabaseLocation, _configuration.Database!)], ct)
                .ConfigureAwait(false);
    }
}