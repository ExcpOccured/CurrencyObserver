using System;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.IntegrationTests.Helpers;

public class CurrencyBuilder
{
    private Currency _currency;

    private CurrencyBuilder()
    {
        _currency = new Currency(
            IdHelper.NextId(),
            CurrencyCode.Undefined.GetRandom(),
            IdHelper.NextDouble(),
            Guid.NewGuid().ToString()[..8],
            DateTime.UtcNow);
    }

    public static CurrencyBuilder CreateDefault()
    {
        return new CurrencyBuilder();
    }

    public Currency Build()
    {
        return _currency;
    }

    public CurrencyBuilder WithValidDate(DateTime validDate)
    {
        _currency = _currency with { ValidDate = validDate };
        return this;
    }

    public CurrencyBuilder WithId(long id)
    {
        _currency = _currency with { Id = id };
        return this;
    }
}