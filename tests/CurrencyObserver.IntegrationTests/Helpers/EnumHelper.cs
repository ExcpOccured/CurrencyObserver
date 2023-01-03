using System;

namespace CurrencyObserver.IntegrationTests.Helpers;

public static class EnumHelper
{
    private static readonly Random Rnd = new(Environment.ProcessId);
    
    public static TEnum GetRandom<TEnum>()
        where TEnum : struct, Enum
    {
        var enumerationValues = Enum.GetValues<TEnum>();
        return (TEnum)enumerationValues.GetValue(Rnd.Next(enumerationValues.Length))!;
    }
}