using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public interface IPgSqlTransactionProvider
{
    Task<NpgsqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}