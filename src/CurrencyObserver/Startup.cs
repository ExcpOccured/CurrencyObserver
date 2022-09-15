using CurrencyObserver.DAL.Extensions;

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
        
        services.AddInternalDataAccessLayer(Configuration);
    }

    public void Configure(
        IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}