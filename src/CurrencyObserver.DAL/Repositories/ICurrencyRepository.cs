using CurrencyObserver.Common.Models;
using Npgsql;

namespace CurrencyObserver.DAL.Repositories;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetAsync(
        NpgsqlTransaction transaction,
        DateTime? toDate,
        CancellationToken cancellationToken);

    Task AddOrUpdateAsync(
        NpgsqlTransaction transaction,
        IEnumerable<Currency> currencies,
        CancellationToken cancellationToken);
}