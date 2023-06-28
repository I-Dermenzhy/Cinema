using Domain.Models;

namespace Contracts.PriceEvalution;

public interface IEvaluationConfiguration
{
    decimal BasePrice { get; }
    Dictionary<string, decimal> MovieGenreCoefficients { get; }
    Dictionary<SeatCategory, decimal> SeatCategoryCoefficients { get; }
}
