using MediatR;

namespace CurrencyObserver.Abstractions.Interfaces.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse> { }