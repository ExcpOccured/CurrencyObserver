namespace CurrencyObserver.Attributes;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumDescriptionAttribute : Attribute
{
    public readonly string Description;

    public EnumDescriptionAttribute(string description)
    {
        Description = description;
    }
}