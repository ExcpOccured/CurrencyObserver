using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;
using static System.Double;

namespace CurrencyObserver.Common.Clients.Models;

[XmlRoot(ElementName = "Valute")]
public class CbrCurrencyResponse
{
    private static readonly NumberFormatInfo ValueDoubleFormat;

    static CbrCurrencyResponse()
    {
        const string commaSeparator = ",";

        ValueDoubleFormat = new NumberFormatInfo
        {
            CurrencyDecimalSeparator = commaSeparator
        };
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlElement(ElementName = "NumCode")] 
    public string NumCode { get; set; } = null!;

    [XmlElement(ElementName = "CharCode")] 
    public string CharCode { get; set; } = null!;

    [XmlElement(ElementName = "Nominal")] 
    public string Nominal { get; set; } = null!;

    [XmlIgnore] 
    public double Value { get; set; }

    [XmlElement(ElementName = "Value")]
    public string ValueSerialized
    {
        get => Value.ToString(CultureInfo.InvariantCulture);
        set
        {
            Debug.Assert(!string.IsNullOrEmpty(value));
            
            TryParse(value, NumberStyles.Currency, ValueDoubleFormat, out var currencyValue);
            Value = currencyValue;
        }
    }

    [XmlAttribute(AttributeName = "ID")] 
    public string Id { get; set; } = null!;
}