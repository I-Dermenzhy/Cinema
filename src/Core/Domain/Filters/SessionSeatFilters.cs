using Domain.Models;

namespace Domain.Filters;

public class SessionSeatFilters : IModelFilters<SessionSeat>
{
    public Seat? Seat { get; set; }
    public Guid? SeatId { get; set; }
    public SeatFilters? SeatFilters { get; set; }

    public Session? Session { get; set; }
    public Guid? SessionId { get; set; }
    public SessionFilters? SessionFilters { get; set; }

    public bool? IsBooked { get; set; }
}
