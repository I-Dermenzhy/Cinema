using Contracts.PriceEvalution;

using Domain.Models;
using Domain.PriceEvalution;

namespace Services.PriceEvaluation;

public class TicketEvaluator : ITicketEvaluator<Ticket>
{
    private readonly IEvaluationConfiguration _configuration;

    public TicketEvaluator(IEvaluationConfiguration configuration) =>
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public virtual decimal EvaluateCost(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));

        return CalculateBasePrice(ticket) * ApplyDiscounts(ticket);
    }

    private static decimal ApplyDiscounts(Ticket ticket) =>
        (decimal)(1 - ticket.Discounts.Sum(t => t.Value));

    private decimal CalculateBasePrice(Ticket ticket) =>
        _configuration.BasePrice *
        GetSeatCategoryCoefficient(ticket.Seat.Category) *
        GetMovieGenreCoefficient(ticket.Session.Movie.Genre);

    private decimal GetMovieGenreCoefficient(string genre) =>
        _configuration.MovieGenreCoefficients.TryGetValue(genre, out decimal coefficient) ?
            coefficient : throw new ArgumentException($"Genre '{genre}' does not have a configured coefficient.");

    private decimal GetSeatCategoryCoefficient(SeatCategory category) =>
        _configuration.SeatCategoryCoefficients.TryGetValue(category, out decimal coefficient) ?
            coefficient : throw new ArgumentException($"Seat category '{category}' does not have a configured coefficient.");
}
