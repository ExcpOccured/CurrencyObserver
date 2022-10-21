using System.Runtime.CompilerServices;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;

namespace CurrencyObserver.DAL.Repositories;

public abstract class PostgresRepositoryBase
{
    private readonly PgOptions _options;

    protected PostgresRepositoryBase(
        IOptions<PgOptions> options)
    {
        _options = options.Value;
    }
    
    protected CancellationTokenSource CreateCancellationTokenSource(CancellationToken cancellationToken = default)
    {
        var timeoutCts = new CancellationTokenSource(_options.Timeout);
        return CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);
    }

    protected string GetQueryName([CallerMemberName] string? caller = null)
    {
        return $"{GetType().Name}.{caller}";
    }
}