using CurrencyObserver.Abstractions.Interfaces;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries.Internal;

public class CurrenciesFromCbrApiQuery : IQuery<IReadOnlyList<Currency>>
{
    public CurrenciesFromCbrApiQuery(Func<Currency, bool>? currenciesFiltrationPredicate)
    {
        CurrenciesFiltrationPredicate = currenciesFiltrationPredicate;
    }

    public Func<Currency, bool>? CurrenciesFiltrationPredicate { get; }
}