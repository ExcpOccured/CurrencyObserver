using System.Linq.Expressions;
using CurrencyObserver.Common.Attributes;

namespace CurrencyObserver.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumVal)
    {
        var field = enumVal.GetType().GetField(enumVal.ToString());

        if (field?.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)
            is not EnumDescriptionAttribute[] attributes)
        {
            return enumVal.ToString();
        }

        return !attributes.IsEmpty()
            ? attributes.First().Description
            : enumVal.ToString();
    }

    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum =>
        IntToEnumConverter<TEnum>.Convert(value);

    private static class IntToEnumConverter<TEnum> where TEnum : Enum
    {
        public static readonly Func<int, TEnum> Convert = GenerateConverter();

        private static Func<int, TEnum> GenerateConverter() => ((Expression<Func<int, TEnum>>)
            (numeric => (TEnum) Enum.ToObject(typeof(TEnum), numeric))).Compile();
    }
}