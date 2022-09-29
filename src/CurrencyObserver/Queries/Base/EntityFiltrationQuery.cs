using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries.Base;

public abstract class EntityFiltrationQuery<TEntity> : IRequest<IReadOnlyList<TEntity>>
    where TEntity : IEntity
{
    public Func<TEntity, bool>? Predicate { get; init; }
}