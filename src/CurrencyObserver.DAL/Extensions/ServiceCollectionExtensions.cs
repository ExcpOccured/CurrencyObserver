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
        configuration.GetSection(CbrClientOptions.Section).Bind(new CbrClientOptions());
        
        services.AddSingleton<ICbrClient, CbrClient>();

        services.AddHttpClient<ICbrClient, CbrClient>();
    }
}