using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class GetCurrenciesFromPgHandler : IRequestHandler<CurrenciesByDateQuery, IReadOnlyList<Currency>>
{
    private readonly ICurrencyRepository _currencyRepository;

    private readonly IPgSqlTransactionProvider _pgSqlTransactionProvider;

    public GetCurrenciesFromPgHandler(
        ICurrencyRepository currencyRepository, 
        IPgSqlTransactionProvider pgSqlTransactionProvider)
    {
        _currencyRepository = currencyRepository;
        _pgSqlTransactionProvider = pgSqlTransactionProvider;
    }

    public async Task<IReadOnlyList<Currency>> Handle(
        CurrenciesByDateQuery query,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _pgSqlTransactionProvider.BeginTransactionAsync(cancellationToken);
        
        var currenciesFromPg = await _currencyRepository.GetAsync(
            transaction,
            query.OnDateTime,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return currenciesFromPg;
    }
}