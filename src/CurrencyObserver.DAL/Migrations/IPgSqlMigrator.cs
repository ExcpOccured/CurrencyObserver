using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IPgSqlMigrator
{
    void ApplyMigrations(
        IPgSqlConnectionProvider pgSqlConnectionProvider,
        ILogger logger);
}