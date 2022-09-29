using JetBrains.Annotations;

namespace CurrencyObserver.DAL.Options;

public abstract class DbOptionsBase
{
    private const int DefaultTimeout = 30_000;
    
    public string ConnectionString { get; [UsedImplicitly] init; } = null!;

    public int Timeout { get; [UsedImplicitly] set; } = DefaultTimeout;
}