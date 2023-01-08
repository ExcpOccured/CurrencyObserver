using CurrencyObserver.Queries;
using FluentValidation;
using JetBrains.Annotations;

namespace CurrencyObserver.Validation.Validators;

[UsedImplicitly]
public class GetCurrenciesByDateQueryValidator : AbstractValidator<GetCurrenciesByDateQuery>
{
    public GetCurrenciesByDateQueryValidator()
    {
        RuleFor(property => property.OnDateTime).Must(BeAValidDateTime);
    }

    private static bool BeAValidDateTime(DateTime dateTime)
    {
        return !dateTime.Equals(default);
    }
}