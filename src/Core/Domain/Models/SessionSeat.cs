using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class SessionSeat : IModel
{
    private bool _isBooked;

    public SessionSeat()
    {
    }

    [SetsRequiredMembers]
    public SessionSeat(Seat seat, Session session, bool isBooked = false)
    {
        Seat = seat ?? throw new ArgumentNullException(nameof(seat));
        Session = session ?? throw new ArgumentNullException(nameof(session));

        SeatId = seat.Id;
        SessionId = session.Id;

        _isBooked = isBooked;
    }

    public event EventHandler<bool>? OnBookingChanged;

    public Guid Id { get; init; }

    public required Seat Seat { get; init; }
    public required Guid SeatId { get; init; }

    public required Session Session { get; init; }
    public required Guid SessionId { get; init; }

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
