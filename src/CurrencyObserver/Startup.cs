using CurrencyObserver.Common;
using CurrencyObserver.Common.Mapping;
using CurrencyObserver.DAL;
using CurrencyObserver.Handlers;
using CurrencyObserver.Handlers.Interfaces;
using CurrencyObserver.Handlers.Internal;
using CurrencyObserver.Handlers.Internal.Interfaces;
using CurrencyObserver.Middleware;
using CurrencyObserver.Validation;
using FluentValidation;
using MediatR;

namespace CurrencyObserver;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddRouting();
        services.AddLogging();
        services.AddSingleton<IMapper, Mapper>();
        services.AddSwaggerGen();

        services.AddSdk();
        services.AddDatabases(Configuration);

        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddControllers();
        ConfigureCqrs(services);
    }

    public void Configure(
        IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

    private static void ConfigureCqrs(IServiceCollection services)
    {
        services.AddMediatR(typeof(Startup));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddTransient<IGetCurrenciesByDateHandler, GetCurrenciesByDateHandler>();
        services.AddTransient<IGetCurrenciesFromCbrApiHandler, GetCurrenciesFromCbrApiHandler>();
        services.AddTransient<IAddOrUpdateCurrenciesInPgHandler, AddOrUpdateCurrenciesInPgHandler>();
    }
}