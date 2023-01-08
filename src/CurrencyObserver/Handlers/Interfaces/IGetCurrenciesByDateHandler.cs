using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Common.Models;
using CurrencyObserver.Queries;

namespace CurrencyObserver.Handlers.Interfaces;

public interface IGetCurrenciesByDateHandler : IQueryHandler<GetCurrenciesByDateQuery, IReadOnlyList<Currency>> { }