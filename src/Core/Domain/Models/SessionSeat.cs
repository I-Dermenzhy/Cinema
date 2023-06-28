using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class SessionSeat : IModel
{
    private bool _isBooked;

    public SessionSeat() { }

    [SetsRequiredMembers]
    public SessionSeat(Seat seat, Session session, bool isBooked = false)
    {
        Seat = seat;
        Session = session;

        SeatId = seat.Id;
        SessionId = session.Id;

        _isBooked = isBooked;
    }

    public event EventHandler<bool>? OnBookingChanged;

    public Guid Id { get; set; }

    public required Seat Seat { get; set; }
    public required Guid SeatId { get; set; }

    public required Session Session { get; set; }
    public required Guid SessionId { get; set; }

    public bool IsBooked
    {
        get => _isBooked;
        set
        {
            if (_isBooked != value)
            {
                OnBookingChanged?.Invoke(this, value);
                _isBooked = value;
            }
        }
    }
}
