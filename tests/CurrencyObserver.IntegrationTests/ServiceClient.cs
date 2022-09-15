using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyObserver.IntegrationTests;

[UsedImplicitly]
public static class ServiceClient
{
    private static readonly IHost TestHost;
    
    static ServiceClient()
    {
        var hostBuilder = Program
            .CreateHostBuilder(Array.Empty<string>())
            .UseEnvironment("Testing")
            .ConfigureWebHost(host => host.UseTestServer());
        TestHost = hostBuilder.Start();
    }

    public static TService GetService<TService>()
        => TestHost.Services.GetService<TService>() 
           ?? throw new ArgumentException($"No such service {nameof(TService)}");
}