using System.Reflection;
using CurrencyObserver.Common.Managers;
using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public class PgSqlMigrator : IPgSqlMigrator
{
    public void ApplyMigrations(
        IEmbeddedResourcesManager embeddedResourcesManager,
        IPgSqlConnectionProvider pgSqlConnectionProvider,
        ILogger logger)
    {
        const string sqlFilesPath = "Migrations.Sql.";

        var assembly = Assembly.GetExecutingAssembly();

        var assemblySqlFiles = embeddedResourcesManager.GetEmbeddedFiles(
            assembly,
            sqlFilesPath);

        using var dbConnection = pgSqlConnectionProvider.OpenConnection();

        var step = 0;
        foreach (var sqlFile in assemblySqlFiles)
        {
            var sqlContent = embeddedResourcesManager.ReadEmbeddedFile(
                assembly,
                sqlFile);
            
            logger.LogInformation("Migration step - [{Step}], Command - {SqlContent}", step, sqlContent);

            using var sqlCommand = dbConnection.CreateCommand();
            sqlCommand.CommandText = sqlContent;
            sqlCommand.ExecuteNonQuery();
            step += 1;
        }
    }
}