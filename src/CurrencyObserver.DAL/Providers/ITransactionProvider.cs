using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public interface ITransactionProvider
{
    Task<NpgsqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}