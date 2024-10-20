using FirebirdSql.Data.FirebirdClient;

namespace FirebirdResources.Aspire.Client;

public class FirebirdConnectionFactory(FirebirdClientSettings settings) : IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private FbConnection? _connection;

    public FirebirdClientSettings Settings => settings;

    public async Task<FbConnection> GetFbConnectionAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            if (_connection is null)
            {
                _connection = new FbConnection(settings.ConnectionString);
                await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            _semaphore.Release();
        }

        return _connection;
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
        _semaphore?.Dispose();
        GC.SuppressFinalize(this);
    }
}
