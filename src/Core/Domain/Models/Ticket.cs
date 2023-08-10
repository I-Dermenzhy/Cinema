using Domain.Exceptions;
using Domain.Models.Discounts;
using Domain.PriceEvalution;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Ticket : IModel, IDiscountable
{
    public Ticket()
    {
    }

    [SetsRequiredMembers]
    public Ticket(Client client, Session session, Seat seat, decimal price)
        : this(client, session, seat)
    {
        Price = price;
    }

    [SetsRequiredMembers]
    public Ticket(Client client, Session session, Seat seat, ITicketEvaluator<Ticket> ticketEvaluation)
        : this(client, session, seat)
    {
        Price = ticketEvaluation.EvaluateCost(this);
    }

    [SetsRequiredMembers]
    private Ticket(Client client, Session session, Seat seat)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        Session = session ?? throw new ArgumentNullException(nameof(session));
        Seat = seat ?? throw new ArgumentNullException(nameof(seat));

        ClientId = client.Id;
        SessionId = session.Id;
        SeatId = seat.Id;
    }

    public Guid Id { get; init; }

    public required Client Client { get; init; }
    public required Guid ClientId { get; init; }

    public required Session Session { get; init; }
    public required Guid SessionId { get; init; }

    public required Seat Seat { get; init; }
    public required Guid SeatId { get; init; }

    public required decimal Price { get; init; }

    public IList<Discount> Discounts { get; init; } = new List<Discount>();

    public void AddDiscount(Discount discount)
    {
        double totalDiscount = Discounts.Sum(discount => discount.Value) + discount.Value;

        if (totalDiscount > 1)
            throw new DiscountExceededException(this, discount);

        Discounts.Add(discount);
    }

    public void RemoveDiscount(Discount discount) => Discounts.Remove(discount);
}