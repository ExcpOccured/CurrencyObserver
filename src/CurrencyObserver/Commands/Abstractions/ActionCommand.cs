using CurrencyObserver.Abstractions.Interfaces.Messaging;
using MediatR;

namespace CurrencyObserver.Commands.Abstractions;

public abstract record ActionCommand : ICommand<Unit> { }