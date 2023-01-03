using System.Threading;

namespace CurrencyObserver.IntegrationTests.Helpers;

internal static class IdHelper
{
    private static long _id = 1;

    public static long NextId() => Interlocked.Increment(ref _id);

    public static double NextDouble() => Interlocked.Increment(ref _id);
}