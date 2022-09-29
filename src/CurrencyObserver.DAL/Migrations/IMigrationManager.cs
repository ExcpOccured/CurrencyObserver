using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IMigrationManager
{
    void ApplyMigrations(
        PgOptions dbOptions,
        ILogger logger);
}