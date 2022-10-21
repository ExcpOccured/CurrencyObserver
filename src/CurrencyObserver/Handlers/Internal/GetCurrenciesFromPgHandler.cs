using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Queries;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class GetCurrenciesFromPgHandler : IRequestHandler<GetCurrenciesByDateQuery, IReadOnlyList<Currency>>
{
    private readonly ICurrencyRepository _currencyRepository;

    private readonly ITransactionProvider _transactionProvider;

    public GetCurrenciesFromPgHandler(
        ICurrencyRepository currencyRepository, 
        ITransactionProvider transactionProvider)
    {
        _currencyRepository = currencyRepository;
        _transactionProvider = transactionProvider;
    }

    public async Task<IReadOnlyList<Currency>> Handle(
        GetCurrenciesByDateQuery query,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _transactionProvider.BeginTransactionAsync(cancellationToken);
        
        var currenciesFromPg = await _currencyRepository.GetLstAsync(
            transaction,
            query.ToDate,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return currenciesFromPg;
    }
}