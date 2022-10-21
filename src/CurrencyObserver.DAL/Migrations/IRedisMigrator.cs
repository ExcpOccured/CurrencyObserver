using CurrencyObserver.Common.Managers;
using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IRedisMigrator
{
    void ApplyMigrations(
        IEmbeddedResourcesManager embeddedResourcesManager,
        IRedisConnectionProvider connectionProvider,
        ILogger logger);
}