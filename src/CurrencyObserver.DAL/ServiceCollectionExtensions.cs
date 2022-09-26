using CurrencyObserver.Common.Clients;
using CurrencyObserver.DAL.Clients;
using CurrencyObserver.DAL.Options;
using CurrencyObserver.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyObserver.DAL;

public static class ServiceCollectionExtensions
{
    public static void AddInternalDataAccessLayer(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<CbrClientOptions>(configuration.GetSection(CbrClientOptions.Section));
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Section));
        
        services.AddHttpClient();
        services.AddSingleton<ICbrClient, CbrClient>();

        services.AddSingleton<ICurrencyRepository, CurrencyRepository>();
    }
}