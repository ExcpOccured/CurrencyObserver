using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public class TransactionProvider : ITransactionProvider
{
    private readonly IConnectionProvider _connectionProvider;

    public TransactionProvider(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Task<NpgsqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return BeginTransactionInternalAsync(cancellationToken);
    }
    
    private async Task<NpgsqlTransaction> BeginTransactionInternalAsync(CancellationToken cancellationToken)
    {
        var connection = await _connectionProvider.OpenConnectionAsync(cancellationToken);
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