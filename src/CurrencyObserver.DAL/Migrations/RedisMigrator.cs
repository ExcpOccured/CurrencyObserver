using System.Reflection;
using CurrencyObserver.Common.Managers;
using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public class RedisMigrator : IRedisMigrator
{
    public void ApplyMigrations(
        IEmbeddedResourcesManager embeddedResourcesManager,
        IRedisConnectionProvider connectionProvider,
        ILogger logger)
    {
        const string redisFilesPath = "Migrations.RedisQueries.";

        var assembly = Assembly.GetExecutingAssembly();

        var assemblyRedisQueries = embeddedResourcesManager.GetEmbeddedFiles(
            assembly,
            redisFilesPath);

        var connectionMultiplexer = connectionProvider.OpenConnection();
        var redisDb = connectionMultiplexer.GetDatabase();

        foreach (var redisQuery in assemblyRedisQueries)
        {
            var queryContent = embeddedResourcesManager.ReadEmbeddedFile(
                assembly,
                redisQuery);

            redisDb.Execute(queryContent);
        }
    }
}