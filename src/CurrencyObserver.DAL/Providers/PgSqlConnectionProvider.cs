using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public class PgSqlConnectionProvider : IPgSqlConnectionProvider
{
    private readonly PgOptions _options;

    public PgSqlConnectionProvider(IOptions<PgOptions> options)
    {
        _options = options.Value;
    }

    public Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        return OpenConnectionInternalAsync(cancellationToken);
    }

    public NpgsqlConnection OpenConnection()
    {
        return OpenConnectionInternal();
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

    private NpgsqlConnection OpenConnectionInternal()
    {
        NpgsqlConnection? connection = null;
        try
        {
            connection = new NpgsqlConnection(_options.ConnectionString);
            connection.Open();
            return connection;
        }
        catch (Exception)
        {
            connection?.Dispose();
            throw;
        }
    }
}