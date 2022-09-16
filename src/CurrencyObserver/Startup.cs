using CurrencyObserver.DAL.Extensions;
using CurrencyObserver.Mapping;
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
        services.AddMediatR(typeof(Startup));
        
        services.AddInternalDataAccessLayer(Configuration);
    }

    public void Configure(
        IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}