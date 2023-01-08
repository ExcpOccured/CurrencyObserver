using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries.Internal;

public record CurrenciesFromPgQuery : IQuery<IReadOnlyList<Currency>>
{
    public DateTime? OnDate { get; init; }
    public CurrencyCode? CurrencyCode { get; init; }
    public Pagination? Pagination { get; init; }
}