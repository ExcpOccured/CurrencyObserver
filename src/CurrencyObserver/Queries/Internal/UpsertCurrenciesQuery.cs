using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries.Internal;

public class UpsertCurrenciesQuery : IRequest
{
    public IReadOnlyList<Currency> Currencies { get; init; } = null!;
}