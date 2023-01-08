using FluentValidation;

namespace CurrencyObserver.Validation.Extensions;

public static class DateTimeValidationExtension
{
    public static IRuleBuilderOptions<TType, DateTime> MustBeAValidDateTime<TType>(
        this IRuleBuilder<TType, DateTime> ruleBuilder)
    {
        return ruleBuilder.Must(BeAValidDateTime);
    }
    
    private static bool BeAValidDateTime(DateTime dateTime)
    {
        return !dateTime.Equals(default);
    }
}