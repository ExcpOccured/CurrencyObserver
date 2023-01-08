using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Handlers.Internal.Interfaces;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class GetCurrenciesFromPgHandler : IGetCurrenciesFromPgHandler
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
        CurrenciesFromPgQuery query,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _pgSqlTransactionProvider.BeginTransactionAsync(cancellationToken);
        
        var currenciesFromPg = await _currencyRepository.GetAsync(
            transaction,
            query.OnDate,
            query.CurrencyCode,
            query.Pagination,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return currenciesFromPg;
    }
}