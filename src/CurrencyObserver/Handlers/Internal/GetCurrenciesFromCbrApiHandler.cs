using System.Collections.Immutable;
using CurrencyObserver.Common.Clients;
using CurrencyObserver.Common.Extensions;
using CurrencyObserver.Common.Mapping;
using CurrencyObserver.Common.Models;
using CurrencyObserver.Queries;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class GetCurrenciesFromCbrApiHandler : IRequestHandler<CurrenciesFiltrationQuery, IReadOnlyList<Currency>>
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
        CurrenciesFiltrationQuery query,
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
            .WhereIf(query.Predicate)
            .OrderBy(currency => currency.Id)
            .ToImmutableList();
    }
}