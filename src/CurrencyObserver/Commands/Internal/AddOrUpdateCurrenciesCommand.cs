using CurrencyObserver.Commands.Abstractions;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Commands.Internal;

public record AddOrUpdateCurrenciesCommand : ActionCommand
{
    public IReadOnlyList<Currency> Currencies { get; init; } = null!;
}