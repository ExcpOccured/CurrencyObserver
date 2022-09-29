using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

public class UpsertCurrenciesToPgHandler : IRequestHandler<UpsertCurrenciesQuery>
{
    private readonly ICurrencyRepository _currencyRepository;

    public UpsertCurrenciesToPgHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<Unit> Handle(
        UpsertCurrenciesQuery query,
        CancellationToken cancellationToken)
    {
        await _currencyRepository.UpsertLstAsync(query.Currencies, cancellationToken);
        return Unit.Value;
    }
}