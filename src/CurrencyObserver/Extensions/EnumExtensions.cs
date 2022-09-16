using CurrencyObserver.Attributes;

namespace CurrencyObserver.Extensions;

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
}