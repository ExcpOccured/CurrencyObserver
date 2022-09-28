namespace CurrencyObserver.Common.Extensions;

public static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        return !source.Any();
    }

    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sequence,
        Func<TSource, bool>? filtrationPredicate)
    {
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (filtrationPredicate is null)
        {
            return sequence;
        }

        return sequence.Where(filtrationPredicate);
    }
}