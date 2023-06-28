using Contracts.PriceEvalution;

using Domain.Models;

namespace Services.PriceEvaluation;

public class TicketEvaluator<T> : ITicketEvaluator, ITicketEvaluator<T> where T : Ticket
{
    private readonly IEvaluationConfiguration _configuration;

    public TicketEvaluator(IEvaluationConfiguration configuration) => _configuration = configuration;

    public decimal EvaluateCost(T ticket) =>
        CalculateBasePrice(ticket) * ApplyDiscounts(ticket);

    public decimal EvaluateCost(Ticket ticket) =>
        ticket is T typedTicket ?
        EvaluateCost(typedTicket) :
        throw new ArgumentNullException(nameof(ticket));

    private decimal ApplyDiscounts(T ticket) =>
        (decimal)(1 - ticket.Discounts.Sum(t => t.Value));

    private decimal CalculateBasePrice(T ticket) =>
        _configuration.BasePrice *
        GetSeatCategoryCoefficient(ticket.Seat.Category) *
        GetMovieGenreCoefficient(ticket.Session.Movie.Genre);

    private decimal GetMovieGenreCoefficient(string genre) =>
        _configuration.MovieGenreCoefficients.TryGetValue(genre, out decimal coefficient) ?
            coefficient : throw new ArgumentException(_configuration.ToString());

    private decimal GetSeatCategoryCoefficient(SeatCategory category) =>
        _configuration.SeatCategoryCoefficients.TryGetValue(category, out decimal coefficient) ?
            coefficient : throw new ArgumentException(_configuration.ToString());
}
