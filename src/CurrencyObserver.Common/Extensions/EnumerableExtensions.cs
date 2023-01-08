namespace CurrencyObserver.Common.Extensions;

public static class EnumerableExtensions
{
    public static bool IsEmpty<TSource>(this IEnumerable<TSource> enumerable)
    {
        return !enumerable.Any();
    }

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> enumerable,
        Func<TSource, bool>? filtrationPredicate)
    {
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (filtrationPredicate is null)
        {
            return enumerable;
        }

        return enumerable.Where(filtrationPredicate);
    }
}