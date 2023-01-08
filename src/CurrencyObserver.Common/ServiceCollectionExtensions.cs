using CurrencyObserver.Common.Managers;
using CurrencyObserver.Common.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyObserver.Common;

public static class ServiceCollectionExtensions
{
    public static void AddSdk(this IServiceCollection services)
    {
        services.AddTransient<AsyncReadWriteLocker>();
        services.AddTransient<IEmbeddedResourcesManager, EmbeddedResourcesManager>();
    }
}