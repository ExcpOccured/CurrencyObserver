using CurrencyObserver.Common.Attributes;

namespace CurrencyObserver.Common.Models;

/// <summary>
/// The currency code that meets the <see cref="CbrCurrencyResponse.CharCode"/>>
/// </summary>
public enum CurrencyCode
{
    /// <summary>
    /// The value is undefined, so it is default value
    /// </summary>
    [EnumDescription("Indefinite")]
    Undefined = 0,
    
    [EnumDescription("Australian Dollar")]
    Aud = 1 << 2,
    
    // ReSharper disable once StringLiteralTypo
    [EnumDescription("Azerbaijani Manat")]
    Azn = 2 << 2,
    
    [EnumDescription("Pound sterling of the United Kingdom")]
    Gbp = 3 << 2,
    
    [EnumDescription("Armenian Drams")]
    Amd = 4 << 2,
    
    [EnumDescription("Belarusian Ruble")]
    Byn = 5 << 2,
    
    [EnumDescription("Bulgarian Lion")]
    Bgn = 6 << 2,
    
    [EnumDescription("Brazilian Real")]
    Brl = 7 << 2,
    
    [EnumDescription("Hungarian Forints")]
    Huf = 8 << 2,
    
    [EnumDescription("Hong Kong Dollars")]
    Hkd = 9 << 2,
    
    [EnumDescription("Danish Crowns")]
    Dkk = 10 << 2,
    
    [EnumDescription("US Dollar")]
    Usd = 11 << 2,
    
    [EnumDescription("Euro")]
    Eur = 12 << 2,
    
    [EnumDescription("Indian Rupees")]
    Inr = 13 << 2,
    
    // ReSharper disable once StringLiteralTypo
    [EnumDescription("Kazakhstani Tenge")]
    Kzt = 14 << 2,
    
    [EnumDescription("Canadian Dollar")]
    Cad = 15 << 2,
    
    // ReSharper disable once StringLiteralTypo
    [EnumDescription("Kyrgyz Som")]
    Kgs = 16 << 2,
    
    [EnumDescription("Chinese Yuan")]
    Cny = 17 << 2,
    
    [EnumDescription("Moldovan lei")]
    Mdl = 18 << 2,
    
    [EnumDescription("Norwegian Crowns")]
    Nok = 19 << 2,
    
    [EnumDescription("Polish Zloty")]
    Pln = 20 << 2,
    
    [EnumDescription("Romanian Leu")]
    Ron = 21 << 2,
    
    [EnumDescription("SDR (Special Drawing Rights)")]
    Xdr = 22 << 2,
    
    [EnumDescription("Singapore Dollar")]
    Sgd = 23 << 2,
    
    // ReSharper disable once StringLiteralTypo
    [EnumDescription("Tajik Somoni")]
    Tjs = 24 << 2,
    
    [EnumDescription("Turkish Lira")]
    Try = 25 << 2,
    
    // ReSharper disable once StringLiteralTypo
    [EnumDescription("New Turkmen Manat")]
    Tmt = 26 << 2,
    
    // ReSharper disable once StringLiteralTypo
    [EnumDescription("Uzbek Soums")]
    Uzs = 27 << 2,
    
    [EnumDescription("Ukrainian hryvnias")]
    Uah = 28 << 2,
    
    [EnumDescription("Czech Crowns")]
    Czk = 29 << 2,
    
    [EnumDescription("Swedish Crowns")]
    Sek = 30 << 2,
    
    [EnumDescription("Swiss Franc")]
    Chf = 31 << 2,
    
    [EnumDescription("South African Rands")]
    Zar = 32 << 2,
    
    [EnumDescription("Won of the Republic of Korea")]
    Krw = 33 << 2,
    
    [EnumDescription("Japanese Yen")]
    Jpy = 34 << 2
}