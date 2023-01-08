using CurrencyObserver.Queries;
using CurrencyObserver.Validation.Extensions;
using FluentValidation;
using JetBrains.Annotations;

namespace CurrencyObserver.Validation.Validators;

[UsedImplicitly]
public class GetCurrencyByCodeQueryValidator : AbstractValidator<GetCurrencyByCodeQuery>
{
    public GetCurrencyByCodeQueryValidator()
    {
        RuleFor(property => property.OnDate).MustBeAValidDateTime().NotEmpty();
        RuleFor(property => property.CurrencyCode).IsInEnum();
    }
}