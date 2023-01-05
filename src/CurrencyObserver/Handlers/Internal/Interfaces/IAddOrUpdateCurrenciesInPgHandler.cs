using CurrencyObserver.Abstractions.Interfaces;
using CurrencyObserver.Commands.Internal;
using MediatR;

namespace CurrencyObserver.Handlers.Internal.Interfaces;

public interface IAddOrUpdateCurrenciesInPgHandler : ICommandHandler<AddOrUpdateCurrenciesCommand, Unit> { }