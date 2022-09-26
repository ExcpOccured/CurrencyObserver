using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries;

public abstract class CurrenciesFiltrationQuery : IRequest<IReadOnlyList<Currency>>
{
    private static bool DefaultFiltrationQuery(Currency currency) => true;

    public Func<Currency, bool> FiltrationPredicate { get; set; } = DefaultFiltrationQuery;
}