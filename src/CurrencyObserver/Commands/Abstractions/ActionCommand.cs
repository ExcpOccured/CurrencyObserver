using CurrencyObserver.Abstractions.Interfaces;
using MediatR;

namespace CurrencyObserver.Commands.Abstractions;

public abstract record ActionCommand : ICommand<Unit> { }