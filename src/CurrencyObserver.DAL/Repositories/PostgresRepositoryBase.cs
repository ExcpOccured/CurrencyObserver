using System.Runtime.CompilerServices;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CurrencyObserver.DAL.Repositories;

public abstract class PostgresRepositoryBase
{
    private readonly PgOptions _options;

    protected PostgresRepositoryBase(
        IOptions<PgOptions> options)
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

    protected Task<NpgsqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return BeginTransactionInternalAsync(cancellationToken);
    }

    protected string GetQueryName([CallerMemberName] string? caller = null)
    {
        return $"{GetType().Name}.{caller}";
    }

    private async Task<NpgsqlConnection> OpenConnectionInternalAsync(CancellationToken cancellationToken)
    {
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

    private async Task<NpgsqlTransaction> BeginTransactionInternalAsync(CancellationToken cancellationToken)
    {
        var connection = await OpenConnectionInternalAsync(cancellationToken);
        try
        {
            var transaction = await connection.BeginTransactionAsync(cancellationToken);
            return transaction;
        }
        catch (Exception)
        {
            connection.Dispose();
            throw;
        }
    }
}