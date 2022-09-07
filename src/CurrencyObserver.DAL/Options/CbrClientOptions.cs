using JetBrains.Annotations;

namespace CurrencyObserver.DAL.Options;

public class CbrClientOptions
{
    public const string Section = "CbrClient";
    
    public string Url { get; [UsedImplicitly] set; } = null!;
}