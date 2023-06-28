using Domain.Models;
using Domain.Models.Discounts;

namespace Domain.Filters;

public class TicketFilters : IModelFilters<Ticket>
{
    public Client? Client { get; set; }
    public Guid? ClientId { get; set; }

    public Session? Session { get; set; }
    public Guid? SessionId { get; set; }

    public Seat? Seat { get; set; }
    public Guid? SeatId { get; set; }

    public IList<Discount>? Discounts { get; set; }
}
