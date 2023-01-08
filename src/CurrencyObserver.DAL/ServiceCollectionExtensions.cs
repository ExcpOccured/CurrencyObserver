using CurrencyObserver.Common.Clients;
using CurrencyObserver.DAL.Clients;
using CurrencyObserver.DAL.Migrations;
using CurrencyObserver.DAL.Options;
using CurrencyObserver.DAL.Providers;
using CurrencyObserver.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyObserver.DAL;

public static class ServiceCollectionExtensions
{
    public static void AddDatabases(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<CbrClientOptions>(configuration.GetSection(CbrClientOptions.Section));
        services.Configure<PgOptions>(configuration.GetSection(PgOptions.Section));
        services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.Section));
        
        services.AddHttpClient();
        services.AddTransient<ICbrClient, CbrClient>();

        services.AddTransient<IPgSqlConnectionProvider, PgSqlConnectionProvider>();
        services.AddTransient<IPgSqlTransactionProvider, PgSqlTransactionProvider>();

        services.AddTransient<IRedisConnectionProvider, RedisConnectionProvider>();
        
        services.AddTransient<IMigrationManager, MigrationManager>();
        services.AddTransient<IPgSqlMigrator, PgSqlMigrator>();
        services.AddTransient<IRedisMigrator, RedisMigrator>();
        
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
    }
}