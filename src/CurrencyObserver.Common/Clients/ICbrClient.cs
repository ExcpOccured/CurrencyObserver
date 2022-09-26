using CurrencyObserver.Common.Clients.Models;

namespace CurrencyObserver.Common.Clients;

public interface ICbrClient
{
    Task<CbrCurrencyQuotesResponse?> GetCurrencyQuotesAsync(CancellationToken cancellationToken);
}