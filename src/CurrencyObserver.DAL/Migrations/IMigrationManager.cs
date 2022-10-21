using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IMigrationManager
{
    void ApplyMigrations(
        IConnectionProvider connectionProvider,
        ILogger logger);
}