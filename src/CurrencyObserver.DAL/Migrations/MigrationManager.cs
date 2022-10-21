using CurrencyObserver.Common.Managers;
using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public class MigrationManager : IMigrationManager
{
    public void ApplyMigrations(
        IServiceProvider services,
        ILogger logger)
    {
        var embeddedResourcesManager = services.GetRequiredService<IEmbeddedResourcesManager>();

        var pgSqlMigrator = services.GetRequiredService<IPgSqlMigrator>();
        var pgSqlConnectionProvider = services.GetRequiredService<IPgSqlConnectionProvider>();

        logger.LogInformation("Start PostgreSQL migrations");

        pgSqlMigrator.ApplyMigrations(
            embeddedResourcesManager,
            pgSqlConnectionProvider,
            logger);

        logger.LogInformation("PostgreSQL migrations applied");

        var redisMigrator = services.GetRequiredService<IRedisMigrator>();
        var redisConnectionProvider = services.GetRequiredService<IRedisConnectionProvider>();

        logger.LogInformation("Start Redis migrations");

        redisMigrator.ApplyMigrations(
            embeddedResourcesManager,
            redisConnectionProvider,
            logger);

        logger.LogInformation("Redis migrations applied");
    }
}