using CurrencyObserver.Common.Extensions;
using CurrencyObserver.Common.Models;
using CurrencyObserver.Queries;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers;

[UsedImplicitly]
public class GetCurrenciesByDateHandler : IRequestHandler<GetCurrenciesByDateQuery, IReadOnlyList<Currency>>
{
    private readonly IMediator _mediator;

    public GetCurrenciesByDateHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IReadOnlyList<Currency>> Handle(
        GetCurrenciesByDateQuery query,
        CancellationToken cancellationToken)
    {
        var toDate = query.ToDate;
        var currenciesFromDb = await _mediator.Send(new CurrenciesByDateQuery
        {
            ToDate = toDate
        }, cancellationToken);

        if (!currenciesFromDb.IsEmpty())
        {
            return currenciesFromDb;
        }

        var currenciesFromCbrApi = await _mediator.Send(new CurrenciesFromCbrApiQuery
        {
            Predicate = currency => DateTime.Equals(currency.AddedAt, toDate)
        }, cancellationToken);
        
        await _mediator.Send(new UpsertCurrenciesQuery
        {
            Currencies = currenciesFromCbrApi
        }, cancellationToken);

        return currenciesFromCbrApi;
    }
}