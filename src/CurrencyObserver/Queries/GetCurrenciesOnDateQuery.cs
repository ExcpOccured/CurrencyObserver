using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries;

public record GetCurrenciesOnDateQuery(DateTime OnDate) : IQuery<IReadOnlyList<Currency>>;