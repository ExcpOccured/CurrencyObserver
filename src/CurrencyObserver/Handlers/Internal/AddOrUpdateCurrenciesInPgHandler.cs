using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries.Internal;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class AddOrUpdateCurrenciesInPgHandler : IRequestHandler<AddOrUpdateCurrenciesQuery>
{
    private readonly ICurrencyRepository _currencyRepository;

    private readonly IPgSqlTransactionProvider _pgSqlTransactionProvider;

    public AddOrUpdateCurrenciesInPgHandler(
        ICurrencyRepository currencyRepository, 
        IPgSqlTransactionProvider pgSqlTransactionProvider)
    {
        _currencyRepository = currencyRepository;
        _pgSqlTransactionProvider = pgSqlTransactionProvider;
    }

    public async Task<Unit> Handle(
        AddOrUpdateCurrenciesQuery query,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _pgSqlTransactionProvider.BeginTransactionAsync(cancellationToken);
        
        await _currencyRepository.AddOrUpdateAsync(
            transaction,
            query.Currencies,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}