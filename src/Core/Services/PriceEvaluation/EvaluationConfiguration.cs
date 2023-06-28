using Contracts.PriceEvalution;

using Domain.Models;

using Microsoft.Extensions.Configuration;

namespace Services.PriceEvaluation;

public class EvaluationConfiguration : IEvaluationConfiguration
{
    private readonly IConfiguration _configuration;

    public EvaluationConfiguration(IConfiguration configuration) => _configuration = configuration;

    public decimal BasePrice => _configuration.GetValue<decimal>("TicketEvaluation:BasePrice");

    public Dictionary<string, decimal> MovieGenreCoefficients =>
        _configuration.GetSection($"TicketEvaluation:MovieGenreCoefficients")
        .Get<Dictionary<string, decimal>>() ?? new Dictionary<string, decimal>();

    public Dictionary<SeatCategory, decimal> SeatCategoryCoefficients =>
        _configuration.GetSection($"TicketEvaluation:SeatCategoryCoefficients")
        .Get<Dictionary<SeatCategory, decimal>>() ?? new Dictionary<SeatCategory, decimal>();
}
