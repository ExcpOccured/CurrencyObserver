using Npgsql;

namespace CurrencyObserver.DAL.Providers;

public interface IConnectionProvider
{
    Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken);

    NpgsqlConnection OpenConnection();
}