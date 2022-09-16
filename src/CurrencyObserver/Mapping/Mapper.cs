using CurrencyObserver.DAL.Clients.Models;
using CurrencyObserver.Extensions;
using CurrencyObserver.Hardcode;
using CurrencyObserver.Models;

namespace CurrencyObserver.Mapping;

public class Mapper : IMapper
{
    public Currency Map(
        string dateFromCbrApi,
        CbrCurrencyResponse currencyFromCbrApi)
    {
        var currencyCode = CurrencyCode.Undefined;

        if (WellKnownCurrencyCodes.CbrCharCodeToCurrencyCodeMap
            .TryGetValue(currencyFromCbrApi.CharCode, out var typedCurrencyCode))
        {
            currencyCode = typedCurrencyCode;
        }

        var date = DateTime.UtcNow;

        if (DateTime.TryParse(dateFromCbrApi, out var parsedDate))
        {
            date = parsedDate;
        }

        return new Currency(
            long.Parse(currencyFromCbrApi.NumCode),
            currencyCode,
            currencyFromCbrApi.Value,
            currencyCode.GetDescription(),
            date);
    }
}