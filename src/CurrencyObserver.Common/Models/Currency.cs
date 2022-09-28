namespace CurrencyObserver.Common.Models;

public record Currency(
    long Id,
    CurrencyCode CurrencyCode,
    double Value,
    string Name,
    DateTime AddedAt) : IEntity;