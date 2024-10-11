using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Firebird.HealthChecks;

/// <summary>
///  Options for <see cref="FirebirdHealthCheck"/>.
/// </summary>
public class FirebirdHealthCheckOptions
{
    /// <summary>
    ///  The Firebird connection string to be used.
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    ///  The query to be executed.
    /// </summary>
    public string CommandText { get; set; } = FirebirdHealthCheckBuilderExtensions.HEALTH_QUERY;

    /// <summary>
    ///  An optional action executed before the connection is opened in the health check.
    /// </summary>
    public Action<FbConnection>? Configure { get; set; }

    /// <summary>
    ///  An optional delegate to build health check result.
    /// </summary>
    public Func<object?, HealthCheckResult>? HealthCheckResultBuilder { get; set; }
}
