using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public interface IPgSqlConnectionProvider
{
    Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken);

    NpgsqlConnection OpenConnection();
}