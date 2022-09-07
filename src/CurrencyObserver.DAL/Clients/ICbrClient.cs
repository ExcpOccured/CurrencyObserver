using CurrencyObserver.DAL.Clients.Models;

namespace CurrencyObserver.DAL.Clients;

public interface ICbrClient
{
    Task<CbrCurrencyQuotesResponse?> GetCurrencyQuotesAsync(CancellationToken cancellationToken);
}