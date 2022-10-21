using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public class PgSqlTransactionProvider : IPgSqlTransactionProvider
{
    private readonly IPgSqlConnectionProvider _pgSqlConnectionProvider;

    public PgSqlTransactionProvider(IPgSqlConnectionProvider pgSqlConnectionProvider)
    {
        _pgSqlConnectionProvider = pgSqlConnectionProvider;
    }

    public Task<NpgsqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return BeginTransactionInternalAsync(cancellationToken);
    }
    
    private async Task<NpgsqlTransaction> BeginTransactionInternalAsync(CancellationToken cancellationToken)
    {
        var connection = await _pgSqlConnectionProvider.OpenConnectionAsync(cancellationToken);
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