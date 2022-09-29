using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class GetCurrenciesFromPgHandler : IRequestHandler<GetCurrenciesByDateQuery, IReadOnlyList<Currency>>
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrenciesFromPgHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<IReadOnlyList<Currency>> Handle(
        GetCurrenciesByDateQuery query,
        CancellationToken cancellationToken)
    {
        var currenciesFromPg = await _currencyRepository.GetLstAsync(
            query.ToDate,
            cancellationToken);

        return currenciesFromPg;
    }
}