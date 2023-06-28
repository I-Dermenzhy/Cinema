using Contracts.PriceEvalution;
using Contracts.Repositories;

using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence.DbContexts;
using Persistence.Repositories;

using Services.PriceEvaluation;

namespace CompositionRoot;

public static class CinemaDI
{
    private static readonly IConfiguration _configuration = GetConfiguration();

    public static IServiceCollection AddCinemaRepositories(this IServiceCollection services) =>
        services.AddDbContext()
            .AddTransient<IClientRepository, ClientRepository>()
            .AddTransient<IMovieRepository, MovieRepository>()
            .AddTransient<ISeatRepository, SeatRepository>()
            .AddTransient<ISessionRepository, SessionRepository>()
            .AddTransient<ITicketRepository, TicketRepository>();

    public static IServiceCollection AddTicketPriceEvaluators(this IServiceCollection services) =>
        services.AddEvaluationConfiguration()
            .AddTransient<ITicketEvaluator, TicketEvaluator<Ticket>>()
            .AddTransient<ITicketEvaluator<Ticket>, TicketEvaluator<Ticket>>();

    private static IServiceCollection AddDbContext(this IServiceCollection services) =>
        services.AddDbContext<CinemaDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("Default")));

    private static IServiceCollection AddEvaluationConfiguration(this IServiceCollection services) =>
        services.AddSingleton(typeof(IEvaluationConfiguration),
            new EvaluationConfiguration(_configuration));

    private static IConfiguration GetConfiguration()
    {
        string baseDirectory = AppContext.BaseDirectory;
        string appSettingsPath = Path.Combine(baseDirectory, "appsettings.json");

        return new ConfigurationBuilder()
            .SetBasePath(baseDirectory)
            .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
            .Build();
    }
}

