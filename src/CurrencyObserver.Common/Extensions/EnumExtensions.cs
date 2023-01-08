using System.Linq.Expressions;
using CurrencyObserver.Common.Attributes;

namespace CurrencyObserver.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumeration)
    {
        var field = enumeration.GetType().GetField(enumeration.ToString());

        if (field?.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)
            is not EnumDescriptionAttribute[] attributes)
        {
            return enumeration.ToString();
        }

        return !attributes.IsEmpty()
            ? attributes.First().Description
            : enumeration.ToString();
    }

    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum =>
        IntToEnumConverter<TEnum>.Convert(value);

    public static int ToInt(this Enum enumeration) => Convert.ToInt32(enumeration);

    private static class IntToEnumConverter<TEnum> where TEnum : Enum
    {
        public static readonly Func<int, TEnum> Convert = GenerateConverter();

        private static Func<int, TEnum> GenerateConverter() => ((Expression<Func<int, TEnum>>)
            (numeric => (TEnum) Enum.ToObject(typeof(TEnum), numeric))).Compile();
    }
}