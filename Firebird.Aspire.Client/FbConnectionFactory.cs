using FirebirdSql.Data.FirebirdClient;

namespace Firebird.Aspire.Client;

public sealed class FbConnectionFactory(FirebirdSettings settings) : IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private FbConnection? _connection;

    public string? ConnectionString => settings.ConnectionString;

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
        _connection?.Dispose();
        _semaphore.Dispose();
    }
}
