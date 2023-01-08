using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Commands.Internal;
using MediatR;

namespace CurrencyObserver.Handlers.Internal.Interfaces;

public interface IAddOrUpdateCurrenciesInPgHandler : ICommandHandler<AddOrUpdateCurrenciesCommand, Unit> { }