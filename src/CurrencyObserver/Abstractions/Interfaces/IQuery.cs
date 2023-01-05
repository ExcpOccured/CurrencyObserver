using MediatR;

namespace CurrencyObserver.Abstractions.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse> { }