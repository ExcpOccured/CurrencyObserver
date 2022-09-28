using CurrencyObserver.Common.Models;
using MediatR;

namespace CurrencyObserver.Queries;

public abstract class EntityFiltrationQuery<TEntity> : IRequest<IReadOnlyList<TEntity>>
    where TEntity : IEntity
{
    public Func<TEntity, bool>? Predicate { get; init; }
}