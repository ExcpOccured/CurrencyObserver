using CurrencyObserver.DAL.Clients;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyObserver.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInternalDataAccessLayer(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<CbrClientOptions>(configuration.GetSection(CbrClientOptions.Section));
        services.AddHttpClient();
        services.AddSingleton<ICbrClient, CbrClient>();
    }
}