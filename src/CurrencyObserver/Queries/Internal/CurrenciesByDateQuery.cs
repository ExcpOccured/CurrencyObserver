using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries.Internal;

public class CurrenciesByDateQuery : IQuery<IReadOnlyList<Currency>>
{
    public DateTime OnDateTime { get; init; }
}