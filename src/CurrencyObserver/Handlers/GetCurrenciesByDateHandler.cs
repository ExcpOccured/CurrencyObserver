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
public class GetCurrenciesByDateHandler : IGetCurrenciesByDateHandler
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
        var onDateTime = query.OnDateTime;
        var currenciesFromDb = await _mediator.Send(new CurrenciesByDateQuery
        {
            OnDateTime = onDateTime
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