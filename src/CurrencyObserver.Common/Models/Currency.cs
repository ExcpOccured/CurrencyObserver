namespace CurrencyObserver.Common.Models;

public record Currency(
    long Id,
    CurrencyCode CurrencyCode,
    double Value,
    string Name,
    // Displays on what date the currency exchange rate record is valid
    DateTime ValidDate) : IEntity;