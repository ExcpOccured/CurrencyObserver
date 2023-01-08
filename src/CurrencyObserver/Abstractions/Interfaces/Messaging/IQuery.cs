using MediatR;

namespace CurrencyObserver.Abstractions.Interfaces.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse> { }