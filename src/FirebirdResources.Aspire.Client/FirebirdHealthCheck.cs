using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FirebirdResources.Aspire.Client;

internal sealed class FirebirdHealthCheck(FirebirdConnectionFactory factory) : IHealthCheck, IDisposable
{
    private const string HealthQuery = "SELECT 1 FROM RDB$DATABASE;";
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            var connection = await factory.GetFbConnectionAsync(cancellationToken);
            using var command = new FbCommand(HealthQuery, connection);
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
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        _semaphore?.Dispose();
        GC.SuppressFinalize(this);
    }
}
