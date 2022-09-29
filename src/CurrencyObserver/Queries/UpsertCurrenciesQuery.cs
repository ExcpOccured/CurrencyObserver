using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries;

public class UpsertCurrenciesQuery : IRequest
{
    public IReadOnlyList<Currency> Currencies { get; init; } = null!;
}