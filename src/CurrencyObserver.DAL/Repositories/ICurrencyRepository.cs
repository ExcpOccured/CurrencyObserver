using CurrencyObserver.Common.Models;
using Npgsql;

namespace CurrencyObserver.DAL.Repositories;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetLstAsync(
        NpgsqlTransaction transaction,
        DateTime? toDate,
        CancellationToken cancellationToken);

    Task UpsertLstAsync(
        NpgsqlTransaction transaction,
        IEnumerable<Currency> currencies,
        CancellationToken cancellationToken);
}