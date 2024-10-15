using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Firebird.Aspire.Client;

internal sealed class FirebirdHealthCheck(FbConnectionFactory factory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using FbConnection connection = await factory.GetFbConnectionAsync(cancellationToken);
            using var transaction = connection.BeginTransaction();
            using var command = new FbCommand("SELECT 1 FROM RDB$DATABASE;", connection, transaction);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                return reader.GetBoolean(0) switch
                {
                    true => HealthCheckResult.Healthy(),
                    _ => HealthCheckResult.Unhealthy()
                };
            }

            return HealthCheckResult.Unhealthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(exception: ex);
        }
    }
}
