using CurrencyObserver.Common.Clients.Models;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Common.Mapping;

public interface IMapper
{
    Currency Map(
        string dateFromCbrApi,
        CbrCurrencyResponse currencyFromCbrApi);
}