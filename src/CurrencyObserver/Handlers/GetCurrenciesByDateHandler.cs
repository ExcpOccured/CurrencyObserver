using CurrencyObserver.Commands.Internal;
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
        var toDate = query.OnDateTime;
        var currenciesFromDb = await _mediator.Send(new CurrenciesByDateQuery
        {
            OnDateTime = toDate
        }, cancellationToken);

        if (!currenciesFromDb.IsEmpty())
        {
            return currenciesFromDb;
        }

        var currenciesFromCbrApi = await _mediator.Send(new CurrenciesFromCbrApiQuery
        {
            Predicate = currency => DateTime.Equals(currency.ValidDate, toDate)
        }, cancellationToken);
        
        await _mediator.Send(new AddOrUpdateCurrenciesCommand
        {
            Currencies = currenciesFromCbrApi
        }, cancellationToken);

        return currenciesFromCbrApi;
    }
}