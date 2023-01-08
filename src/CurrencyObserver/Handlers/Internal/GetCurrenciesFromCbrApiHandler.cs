using System.Collections.Immutable;
using CurrencyObserver.Common.Clients;
using CurrencyObserver.Common.Extensions;
using CurrencyObserver.Common.Mapping;
using CurrencyObserver.Common.Models;
using CurrencyObserver.Handlers.Internal.Interfaces;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class GetCurrenciesFromCbrApiHandler : IGetCurrenciesFromCbrApiHandler
{
    private readonly ICbrClient _cbrClient;

    private readonly IMapper _mapper;

    public GetCurrenciesFromCbrApiHandler(
        ICbrClient cbrClient,
        IMapper mapper)
    {
        _cbrClient = cbrClient;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<Currency>> Handle(
        CurrenciesFromCbrApiQuery query,
        CancellationToken cancellationToken)
    {
        var currenciesFromCbrApi = await _cbrClient.GetCurrencyQuotesAsync(cancellationToken);

        if (currenciesFromCbrApi is null)
        {
            return ArraySegment<Currency>.Empty;
        }

        var cbrCurrenciesDate = currenciesFromCbrApi.Date;
        return currenciesFromCbrApi
            .Currencies
            .Select(currencyFromCbrApi => _mapper.Map(cbrCurrenciesDate, currencyFromCbrApi))
            .WhereIf(query.CurrenciesFiltrationPredicate)
            .OrderBy(currency => currency.Id)
            .ToImmutableList();
    }
}