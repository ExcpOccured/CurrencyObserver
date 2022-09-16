namespace CurrencyObserver.Models;

public record Currency(
    long Id, 
    CurrencyCode CurrencyCode,
    double Value,
    string Name,
    DateTime UpdateAt);