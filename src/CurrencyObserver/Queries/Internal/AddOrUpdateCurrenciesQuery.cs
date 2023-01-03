using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries.Internal;

public class AddOrUpdateCurrenciesQuery : IRequest
{
    public IReadOnlyList<Currency> Currencies { get; init; } = null!;
}