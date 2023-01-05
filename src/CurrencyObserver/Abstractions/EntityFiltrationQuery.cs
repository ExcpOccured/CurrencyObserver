using CurrencyObserver.Abstractions.Interfaces;
using CurrencyObserver.Common.Models;

namespace CurrencyObserver.Abstractions;

public abstract record EntityFiltrationQuery<TEntity> : IQuery<IReadOnlyList<TEntity>>
    where TEntity : IEntity
{
    public Func<TEntity, bool>? Predicate { get; init; }
}