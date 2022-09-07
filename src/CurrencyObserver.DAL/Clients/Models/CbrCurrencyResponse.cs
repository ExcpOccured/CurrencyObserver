using System.Xml.Serialization;

namespace CurrencyObserver.DAL.Clients.Models;

[XmlRoot(ElementName="Valute")]
public class CbrCurrencyResponse
{
    [XmlElement(ElementName="NumCode")]
    public string NumCode { get; set; } = null!;

    [XmlElement(ElementName="CharCode")]
    public string CharCode { get; set; } = null!;

    [XmlElement(ElementName="Nominal")]
    public string Nominal { get; set; } = null!;
    
    [XmlElement(ElementName="Value")]
    public double Value { get; set; }
    
    [XmlAttribute(AttributeName="ID")]
    public string Id { get; set; } = null!;
}