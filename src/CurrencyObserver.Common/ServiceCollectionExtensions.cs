using CurrencyObserver.Common.Managers;
using CurrencyObserver.Common.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyObserver.Common;

public static class ServiceCollectionExtensions
{
    public static void AddCommon(this IServiceCollection services)
    {
        services.AddSingleton<AsyncReadWriteLocker>();
        services.AddSingleton<IEmbeddedResourcesManager, EmbeddedResourcesManager>();
    }
}