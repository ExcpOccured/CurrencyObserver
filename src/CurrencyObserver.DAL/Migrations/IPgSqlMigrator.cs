using CurrencyObserver.Common.Managers;
using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IPgSqlMigrator
{
    void ApplyMigrations(
        IEmbeddedResourcesManager embeddedResourcesManager,
        IPgSqlConnectionProvider pgSqlConnectionProvider,
        ILogger logger);
}