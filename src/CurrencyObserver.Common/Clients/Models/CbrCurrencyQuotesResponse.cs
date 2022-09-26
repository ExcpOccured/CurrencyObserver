using System.Xml.Serialization;

namespace CurrencyObserver.Common.Clients.Models;

[XmlRoot(ElementName = "ValCurs")]
public class CbrCurrencyQuotesResponse
{
    [XmlElement(ElementName = "Valute")] 
    public List<CbrCurrencyResponse> Currencies { get; set; } = null!;

    [XmlAttribute(AttributeName = "Date")] 
    public string Date { get; set; } = null!;

    [XmlAttribute(AttributeName = "name")] 
    public string Name { get; set; } = null!;
}