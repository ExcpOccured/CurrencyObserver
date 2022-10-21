using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public class PgSqlMigrator : IPgSqlMigrator
{
    public void ApplyMigrations(
        IPgSqlConnectionProvider pgSqlConnectionProvider,
        ILogger logger)
    {
        throw new NotImplementedException();
    }
}