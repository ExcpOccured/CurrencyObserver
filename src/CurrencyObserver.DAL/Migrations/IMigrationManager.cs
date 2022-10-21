using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IMigrationManager
{
    void ApplyMigrations(
        IServiceProvider services,
        ILogger logger);
}