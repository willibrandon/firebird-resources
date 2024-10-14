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
            // The factory connects (and opens).
            _ = await factory.GetFbConnectionAsync(cancellationToken);

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(exception: ex);
        }
    }
}
