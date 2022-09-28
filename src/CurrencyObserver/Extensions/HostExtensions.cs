using CurrencyObserver.DAL.Migrations;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;

namespace CurrencyObserver.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase(this IHost host, string[] args = null!)
    {
        using var scope = host.Services.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
        var dbOptions = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
        var migrationManager = scope.ServiceProvider.GetRequiredService<IMigrationManager>();

        try
        {
            migrationManager.ApplyMigrations(dbOptions.Value, logger);
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