using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries;

public class GetCurrenciesByDateQuery : IRequest<IReadOnlyList<Currency>>
{
    public DateTime ToDate { get; init; }
}