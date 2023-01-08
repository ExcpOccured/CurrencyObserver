using CurrencyObserver.Common.Models;
using CurrencyObserver.Handlers.Interfaces;
using CurrencyObserver.Queries;
using CurrencyObserver.Queries.Internal;
using MediatR;

namespace CurrencyObserver.Handlers;

public class GetCurrencyByCodeHandler : IGetCurrencyByCodeHandler
{
    private readonly IMediator _mediator;

    public GetCurrencyByCodeHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Currency?> Handle(
        GetCurrencyByCodeQuery query,
        CancellationToken cancellationToken)
    {
        var currenciesFromDb = await _mediator.Send(new CurrenciesFromPgQuery
        {
            OnDate = query.OnDate,
            CurrencyCode = query.CurrencyCode
        }, cancellationToken);

        return currenciesFromDb.FirstOrDefault();
    }
}