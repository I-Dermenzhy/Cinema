using Contracts.PriceEvalution;

using Domain.Models;

using Microsoft.Extensions.Configuration;

using Services.Exceptions;

namespace Services.PriceEvaluation;

public class EvaluationConfiguration : IEvaluationConfiguration
{
    private const string MovieGenreCoefficientsConfigurationPath = "TicketEvaluation:MovieGenreCoefficients";
    private const string SeatCategoryCoefficientsConfigurationPath = "TicketEvaluation:SeatCategoryCoefficients";

    private readonly IConfiguration _configuration;

    public EvaluationConfiguration(IConfiguration configuration) =>
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public decimal BasePrice => _configuration.GetValue<decimal>("TicketEvaluation:BasePrice");

    public Dictionary<string, decimal> MovieGenreCoefficients =>
        _configuration.GetSection(MovieGenreCoefficientsConfigurationPath)
            .Get<Dictionary<string, decimal>>() ?? throw new ConfigurationSectionNotFoundException(_configuration,
                $"No coefficient found with the following path: {MovieGenreCoefficientsConfigurationPath} in the given configuration");

    public Dictionary<SeatCategory, decimal> SeatCategoryCoefficients =>
        _configuration.GetSection(SeatCategoryCoefficientsConfigurationPath)
            .Get<Dictionary<SeatCategory, decimal>>() ?? throw new ConfigurationSectionNotFoundException(_configuration,
                $"No coefficient found with the following path: {SeatCategoryCoefficientsConfigurationPath} in the given configuration");
}
