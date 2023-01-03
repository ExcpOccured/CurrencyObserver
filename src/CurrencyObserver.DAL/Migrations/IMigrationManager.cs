using CurrencyObserver.Common.Managers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IMigrationManager
{
    void ApplyPgSqlMigrations(
        IEmbeddedResourcesManager embeddedResourcesManager,
        IServiceProvider services,
        ILogger logger);

    void ApplyRedisMigrations(
        IEmbeddedResourcesManager embeddedResourcesManager,
        IServiceProvider services,
        ILogger logger);
}