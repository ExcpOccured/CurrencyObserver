using CurrencyObserver.Commands.Internal;
using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using CurrencyObserver.Handlers.Internal.Interfaces;
using JetBrains.Annotations;
using MediatR;

namespace CurrencyObserver.Handlers.Internal;

[UsedImplicitly]
public class AddOrUpdateCurrenciesInPgHandler : IAddOrUpdateCurrenciesInPgHandler
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
        AddOrUpdateCurrenciesCommand command, 
        CancellationToken cancellationToken)
    {
        await using var transaction = await _pgSqlTransactionProvider.BeginTransactionAsync(cancellationToken);
        
        await _currencyRepository.AddOrUpdateAsync(
            transaction,
            command.Currencies,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}