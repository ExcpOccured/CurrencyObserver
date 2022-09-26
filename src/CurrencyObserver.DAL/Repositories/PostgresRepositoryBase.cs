using System.Runtime.CompilerServices;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CurrencyObserver.DAL.Repositories;

public abstract class PostgresRepositoryBase
{
    private readonly DatabaseOptions _options;

    protected PostgresRepositoryBase(
        IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
    }

    protected Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        return OpenConnectionInternalAsync(cancellationToken);
    }

    protected CancellationTokenSource CreateCancellationTokenSource(CancellationToken cancellationToken = default)
    {
        var timeoutCts = new CancellationTokenSource(_options.Timeout);
        return CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);
    }
    
    protected string GetQueryName([CallerMemberName] string? caller = null)
    {
        return $"{GetType().Name}.{caller}";
    }

    private async Task<NpgsqlConnection> OpenConnectionInternalAsync(CancellationToken cancellationToken)
    {
        using var cts = CreateCancellationTokenSource(cancellationToken);

        NpgsqlConnection? connection = null;
        try
        {
            connection = new NpgsqlConnection(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
        catch (Exception)
        {
            connection?.Dispose();
            throw;
        }
    }
}