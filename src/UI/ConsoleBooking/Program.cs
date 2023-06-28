using CompositionRoot;

using ConsoleBooking;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();
using IServiceScope scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

var app = services.GetRequiredService<App>();
await app.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services
                .AddCinemaRepositories()
                .AddTicketPriceEvaluators()
                .AddTransient<App>();
        });
