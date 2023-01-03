using CurrencyObserver.Common.Managers;
using CurrencyObserver.DAL.Migrations;

namespace CurrencyObserver.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabases(this IHost host, string[] args = null!)
    {
        using var scope = host.Services.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationManager>>();
        var migrationManager = scope.ServiceProvider.GetRequiredService<IMigrationManager>();
        
        var embeddedResourcesManager = scope.ServiceProvider.GetRequiredService<IEmbeddedResourcesManager>();

        try
        {
            migrationManager.ApplyPgSqlMigrations(
                embeddedResourcesManager,
                scope.ServiceProvider,
                logger);
            
            /*
            migrationManager.ApplyRedisMigrations(
                embeddedResourcesManager,
                scope.ServiceProvider,
                logger);
            */
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