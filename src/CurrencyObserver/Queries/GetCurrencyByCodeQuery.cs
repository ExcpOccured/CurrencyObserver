using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries;

public record GetCurrencyByCodeQuery(DateTime OnDate, CurrencyCode CurrencyCode) : IQuery<Currency?>;