using CurrencyObserver;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(hostBuilder =>
    {
        hostBuilder.UseStartup<Startup>();
        hostBuilder.SuppressStatusMessages(true);
    });

var app = builder.Build();
app.Run();