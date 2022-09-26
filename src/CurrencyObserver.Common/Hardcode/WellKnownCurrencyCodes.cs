using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Common.Hardcode;

// TODO: Move this to Redis
public static class WellKnownCurrencyCodes
{
    public static readonly Dictionary<string, CurrencyCode> CbrCharCodeToCurrencyCodeMap;

    static WellKnownCurrencyCodes()
    {
        CbrCharCodeToCurrencyCodeMap = new Dictionary<string, CurrencyCode>
        {
            [nameof(CurrencyCode.Aud).ToUpper()] = CurrencyCode.Aud,
            [nameof(CurrencyCode.Azn).ToUpper()] = CurrencyCode.Azn,
            [nameof(CurrencyCode.Gbp).ToUpper()] = CurrencyCode.Gbp,
            [nameof(CurrencyCode.Amd).ToUpper()] = CurrencyCode.Amd,
            [nameof(CurrencyCode.Byn).ToUpper()] = CurrencyCode.Byn,
            [nameof(CurrencyCode.Bgn).ToUpper()] = CurrencyCode.Bgn,
            [nameof(CurrencyCode.Brl).ToUpper()] = CurrencyCode.Brl,
            [nameof(CurrencyCode.Huf).ToUpper()] = CurrencyCode.Huf,
            [nameof(CurrencyCode.Hkd).ToUpper()] = CurrencyCode.Hkd,
            [nameof(CurrencyCode.Dkk).ToUpper()] = CurrencyCode.Dkk,
            [nameof(CurrencyCode.Usd).ToUpper()] = CurrencyCode.Usd,
            [nameof(CurrencyCode.Eur).ToUpper()] = CurrencyCode.Eur,
            [nameof(CurrencyCode.Inr).ToUpper()] = CurrencyCode.Inr,
            [nameof(CurrencyCode.Kzt).ToUpper()] = CurrencyCode.Kzt,
            [nameof(CurrencyCode.Cad).ToUpper()] = CurrencyCode.Cad,
            [nameof(CurrencyCode.Kgs).ToUpper()] = CurrencyCode.Kgs,
            [nameof(CurrencyCode.Cny).ToUpper()] = CurrencyCode.Cny,
            [nameof(CurrencyCode.Mdl).ToUpper()] = CurrencyCode.Mdl,
            [nameof(CurrencyCode.Nok).ToUpper()] = CurrencyCode.Nok,
            [nameof(CurrencyCode.Pln).ToUpper()] = CurrencyCode.Pln,
            [nameof(CurrencyCode.Ron).ToUpper()] = CurrencyCode.Ron,
            [nameof(CurrencyCode.Xdr).ToUpper()] = CurrencyCode.Xdr,
            [nameof(CurrencyCode.Sgd).ToUpper()] = CurrencyCode.Sgd,
            [nameof(CurrencyCode.Tjs).ToUpper()] = CurrencyCode.Tjs,
            [nameof(CurrencyCode.Try).ToUpper()] = CurrencyCode.Try,
            [nameof(CurrencyCode.Tmt).ToUpper()] = CurrencyCode.Tmt,
            [nameof(CurrencyCode.Uzs).ToUpper()] = CurrencyCode.Uzs,
            [nameof(CurrencyCode.Uah).ToUpper()] = CurrencyCode.Uah,
            [nameof(CurrencyCode.Czk).ToUpper()] = CurrencyCode.Czk,
            [nameof(CurrencyCode.Sek).ToUpper()] = CurrencyCode.Sek,
            [nameof(CurrencyCode.Chf).ToUpper()] = CurrencyCode.Chf,
            [nameof(CurrencyCode.Zar).ToUpper()] = CurrencyCode.Zar,
            [nameof(CurrencyCode.Krw).ToUpper()] = CurrencyCode.Krw,
            [nameof(CurrencyCode.Jpy).ToUpper()] = CurrencyCode.Jpy
        };
    }
}