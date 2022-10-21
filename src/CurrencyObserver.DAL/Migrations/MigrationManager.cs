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
        var pgSqlMigrator = services.GetRequiredService<IPgSqlMigrator>();
        var pgSqlConnectionProvider = services.GetRequiredService<IPgSqlConnectionProvider>();
        
        logger.LogInformation("Start PostgreSQL migrations");

        pgSqlMigrator.ApplyMigrations(pgSqlConnectionProvider, logger);
        
        logger.LogInformation("PostgreSQL migrations applied");
        
        
    }
}