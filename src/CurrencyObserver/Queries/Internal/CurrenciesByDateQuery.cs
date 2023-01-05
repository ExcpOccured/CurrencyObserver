using CurrencyObserver.Abstractions.Interfaces;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries.Internal;

public class CurrenciesByDateQuery : IQuery<IReadOnlyList<Currency>>
{
    public DateTime OnDateTime { get; init; }
}