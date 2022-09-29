﻿using CurrencyObserver.Common.Clients;
using CurrencyObserver.DAL.Clients;
using CurrencyObserver.DAL.Migrations;
using CurrencyObserver.DAL.Options;
using CurrencyObserver.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyObserver.DAL;

public static class ServiceCollectionExtensions
{
    public static void AddDatabase(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<CbrClientOptions>(configuration.GetSection(CbrClientOptions.Section));
        services.Configure<PgOptions>(configuration.GetSection(PgOptions.Section));
        services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.Section));
        
        services.AddHttpClient();
        services.AddSingleton<ICbrClient, CbrClient>();

        services.AddSingleton<IMigrationManager, MigrationManager>();
        services.AddSingleton<ICurrencyRepository, CurrencyRepository>();
    }
}