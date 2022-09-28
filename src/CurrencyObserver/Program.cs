using CurrencyObserver.Extensions;

namespace CurrencyObserver;

public static class Program
{
    private const string SettingsFile = "appsettings.json";
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .MigrateDatabase(args)
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(hostBuilder =>
            {
                hostBuilder.UseStartup<Startup>();
                hostBuilder.SuppressStatusMessages(true);
            })
            .ConfigureAppConfiguration(app =>
            {
                app.AddJsonFile(SettingsFile);
            });
    }
}