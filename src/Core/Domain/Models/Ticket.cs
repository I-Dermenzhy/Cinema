using Domain.Exceptions;
using Domain.Models.Discounts;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Ticket : IModel, IDiscountable
{
    public Ticket() { }

    [SetsRequiredMembers]
    public Ticket(Client client, Session session, Seat seat, decimal price)
    {
        Client = client;
        Session = session;
        Seat = seat;
        Price = price;

        ClientId = client.Id;
        SessionId = session.Id;
        SeatId = seat.Id;
    }

    public Guid Id { get; set; }

    public required Client Client { get; set; }
    public required Guid ClientId { get; set; }

    public required Session Session { get; set; }
    public required Guid SessionId { get; set; }

    public required Seat Seat { get; set; }
    public required Guid SeatId { get; set; }

    public required decimal Price { get; set; }

    public IList<Discount> Discounts { get; set; } = new List<Discount>();

    public void AddDiscount(Discount discount)
    {
        double totalDiscount = Discounts.Sum(discount => discount.Value) + discount.Value;

        if (totalDiscount > 1)
            throw new DiscountExceededException(this, discount);

        Discounts.Add(discount);
    }

    public void RemoveDiscount(Discount discount) => Discounts.Remove(discount);
}