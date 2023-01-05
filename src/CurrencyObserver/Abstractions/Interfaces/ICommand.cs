using MediatR;

namespace CurrencyObserver.Abstractions.Interfaces;

public interface ICommand<out TResponse> : IRequest<TResponse> { }