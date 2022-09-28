using CurrencyObserver.Common.Clients.Models;
using CurrencyObserver.Common.Exceptions;
using CurrencyObserver.Common.Extensions;
using CurrencyObserver.Common.Hardcode;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Common.Mapping;

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

        if (!long.TryParse(currencyFromCbrApi.NumCode, out var parsedCurrencyId))
        {
            throw new FailedToParseNumCodeException(currencyFromCbrApi.NumCode);
        }

        return new Currency(
            parsedCurrencyId,
            currencyCode,
            currencyFromCbrApi.Value,
            currencyCode.GetDescription(),
            date);
    }
}