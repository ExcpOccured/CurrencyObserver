using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class UpsertCurrenciesToPgHandler : IRequestHandler<UpsertCurrenciesQuery>
{
    private readonly ICurrencyRepository _currencyRepository;

    private readonly IPgSqlTransactionProvider _pgSqlTransactionProvider;

    public UpsertCurrenciesToPgHandler(
        ICurrencyRepository currencyRepository, 
        IPgSqlTransactionProvider pgSqlTransactionProvider)
    {
        _currencyRepository = currencyRepository;
        _pgSqlTransactionProvider = pgSqlTransactionProvider;
    }

    public async Task<Unit> Handle(
        UpsertCurrenciesQuery query,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _pgSqlTransactionProvider.BeginTransactionAsync(cancellationToken);
        
        await _currencyRepository.UpsertLstAsync(
            transaction,
            query.Currencies,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}