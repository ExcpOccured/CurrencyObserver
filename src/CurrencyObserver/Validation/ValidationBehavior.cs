using CurrencyObserver.Common.Extensions;
using FluentValidation;
using MediatR;

namespace CurrencyObserver.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> continueTask,
        CancellationToken cancellationToken)
    {
        if (_validators.IsEmpty())
        {
            return await continueTask();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationFailures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(validationOffset => validationOffset.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();

        if (!validationFailures.IsEmpty())
        {
            throw new ValidationException(validationFailures);
        }

        return await continueTask();
    }
}