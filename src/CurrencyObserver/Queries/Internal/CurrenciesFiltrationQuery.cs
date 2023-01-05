using CurrencyObserver.Abstractions;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Queries.Internal;

public record CurrenciesFiltrationQuery : EntityFiltrationQuery<Currency> { }