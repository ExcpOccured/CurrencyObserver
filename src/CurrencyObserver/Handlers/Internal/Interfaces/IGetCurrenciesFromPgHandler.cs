using CurrencyObserver.Abstractions.Interfaces.Messaging;
using CurrencyObserver.Common.Models;
using CurrencyObserver.Queries.Internal;

namespace CurrencyObserver.Handlers.Internal.Interfaces;

public interface IGetCurrenciesFromPgHandler : IQueryHandler<CurrenciesByDateQuery, IReadOnlyList<Currency>> { }