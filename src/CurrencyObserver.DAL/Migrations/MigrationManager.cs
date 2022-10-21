using System.Reflection;
using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public class MigrationManager : IMigrationManager
{
    public void ApplyMigrations(
        IPgSqlConnectionProvider pgSqlConnectionProvider,
        ILogger logger)
    {
        const string sqlFilesPath = "Migrations.Sql.";

        var assembly = Assembly.GetExecutingAssembly();

        var assemblySqlFiles = GetExecutableSqlFiles(
            assembly,
            sqlFilesPath);

        using var dbConnection = pgSqlConnectionProvider.OpenConnection();

        foreach (var sqlFile in assemblySqlFiles)
        {
            var sqlContent = ReadEmbeddedResource(assembly, sqlFile);

            using var sqlCommand = dbConnection.CreateCommand();
            sqlCommand.CommandText = sqlContent;
            sqlCommand.ExecuteNonQuery();
        }
    }

    private static string ReadEmbeddedResource(
        Assembly assembly,
        string resource)
    {
        using var stream = assembly.GetManifestResourceStream(resource);

        if (stream is null)
        {
            throw new InvalidOperationException($"Resource {resource} not found");
        }

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private static IEnumerable<string> GetExecutableSqlFiles(
        Assembly sqlFilesAssembly,
        string? sqlFilesPath) =>
        sqlFilesAssembly
            .GetManifestResourceNames()
            .Where(str => str.StartsWith($"{sqlFilesAssembly.GetName().Name}.{sqlFilesPath}"))
            .OrderBy(str => str);
}