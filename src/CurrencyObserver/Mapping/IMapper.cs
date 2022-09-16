using CurrencyObserver.DAL.Clients.Models;
using CurrencyObserver.Models;

namespace CurrencyObserver.Mapping;

public interface IMapper
{
    Currency Map(
        string dateFromCbrApi,
        CbrCurrencyResponse currencyFromCbrApi);
}