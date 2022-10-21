using StackExchange.Redis;

namespace CurrencyObserver.DAL.Providers;

public interface IRedisConnectionProvider
{
    Task<ConnectionMultiplexer> OpenConnectionAsync(CancellationToken cancellationToken);
}