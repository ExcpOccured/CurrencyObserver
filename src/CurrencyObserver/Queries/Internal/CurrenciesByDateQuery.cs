using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries.Internal;

public class CurrenciesByDateQuery : IRequest<IReadOnlyList<Currency>>
{
    public DateTime ToDate { get; init; }
}