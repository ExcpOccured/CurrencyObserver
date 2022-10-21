using CurrencyObserver.DAL.Migrations;
using CurrencyObserver.DAL.Providers;

namespace CurrencyObserver.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase(this IHost host, string[] args = null!)
    {
        using var scope = host.Services.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationManager>>();
        var migrationManager = scope.ServiceProvider.GetRequiredService<IMigrationManager>();
        var connectionProvider = scope.ServiceProvider.GetRequiredService<IPgSqlConnectionProvider>();

        try
        {
            migrationManager.ApplyMigrations(connectionProvider, logger);
        }
        catch (Exception exception)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            logger.LogError(exception, exception.Message);
            throw;
        }

        return host;
    }
}