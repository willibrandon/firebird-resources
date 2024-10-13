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
    /// Gets the FirebirdSql connection string.
    /// </summary>
    /// <returns>The FirebirdSql connection string.</returns>
    public string GetConnectionString()
    {
        string database = _configuration.Database != null
            ? Path.Combine(FirebirdBuilder.DefaultDatabaseLocation, _configuration.Database)
            : FirebirdBuilder.DefaultDatabase; ;

        var properties = new Dictionary<string, string>
        {
            { "DataSource", Hostname },
            { "Port", GetMappedPublicPort(FirebirdBuilder.Port).ToString() },
            { "Database", database },
        };

        if (_configuration.Username != null)
        {
            properties.Add("User", _configuration.Username);
        }
        else
        {
            properties.Add("User", FirebirdBuilder.DefaultUsername);
        }

        if (_configuration.Password != null)
        {
            properties.Add("Password", _configuration.Password);
        }
        else
        {
            properties.Add("Password", FirebirdBuilder.DefaultPassword);
        }

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