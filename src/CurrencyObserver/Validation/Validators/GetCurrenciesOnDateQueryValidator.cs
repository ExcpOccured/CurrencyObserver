using CurrencyObserver.Queries;
using CurrencyObserver.Validation.Extensions;
using FluentValidation;
using JetBrains.Annotations;

namespace CurrencyObserver.Validation.Validators;

[UsedImplicitly]
public class GetCurrenciesOnDateQueryValidator : AbstractValidator<GetCurrenciesOnDateQuery>
{
    public GetCurrenciesOnDateQueryValidator()
    {
        RuleFor(property => property.OnDate).MustBeAValidDateTime().NotEmpty();
    }
}