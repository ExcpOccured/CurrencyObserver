using CurrencyObserver.Commands.Internal;
using CurrencyObserver.Common.Extensions;
using CurrencyObserver.Common.Models;
using CurrencyObserver.Handlers.Interfaces;
using CurrencyObserver.Queries;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers;

[UsedImplicitly]
public class GetCurrenciesOnDateHandler : IGetCurrenciesByDateHandler
{
    private readonly IMediator _mediator;

    public GetCurrenciesOnDateHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IReadOnlyList<Currency>> Handle(
        GetCurrenciesOnDateQuery query,
        CancellationToken cancellationToken)
    {
        var onDateTime = query.OnDate;
        var currenciesFromDb = await _mediator.Send(new CurrenciesFromPgQuery
        {
            OnDate = onDateTime
        }, cancellationToken);

        if (!currenciesFromDb.IsEmpty())
        {
            return currenciesFromDb;
        }

        var currenciesFromCbrApi = await _mediator.Send(
            new CurrenciesFromCbrApiQuery(
            currency => DateTime.Equals(currency.ValidDate, onDateTime)),
            cancellationToken);
        
        await _mediator.Send(new AddOrUpdateCurrenciesCommand
        {
            Currencies = currenciesFromCbrApi
        }, cancellationToken);

        return currenciesFromCbrApi;
    }
}