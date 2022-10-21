using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class UpsertCurrenciesToPgHandler : IRequestHandler<UpsertCurrenciesQuery>
{
    private readonly ICurrencyRepository _currencyRepository;

    private readonly ITransactionProvider _transactionProvider;

    public UpsertCurrenciesToPgHandler(
        ICurrencyRepository currencyRepository, 
        ITransactionProvider transactionProvider)
    {
        _currencyRepository = currencyRepository;
        _transactionProvider = transactionProvider;
    }

    public async Task<Unit> Handle(
        UpsertCurrenciesQuery query,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _transactionProvider.BeginTransactionAsync(cancellationToken);
        
        await _currencyRepository.UpsertLstAsync(
            transaction,
            query.Currencies,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}