using JetBrains.Annotations;

namespace CurrencyObserver.DAL.Options;

public class DatabaseOptions
{
    public const string Section = "Database";
    
    private const int DefaultTimeout = 30_000;

    public string ConnectionString { get; [UsedImplicitly] set; } = null!;

    public int Timeout { get; [UsedImplicitly] set; } = DefaultTimeout;
}