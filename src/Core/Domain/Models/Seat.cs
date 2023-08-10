using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Seat : IModel
{
    private int _row;
    private int _place;

    public Seat()
    {
    }

    [SetsRequiredMembers]
    public Seat(int row, int place, SeatCategory category)
    {
        Row = row;
        Place = place;
        Category = category;
    }

    public Guid Id { get; init; }

    public int Row
    {
        get => _row;
        init
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(Row), "Row must have a positive value.");

            _row = value;
        }
    }

    public int Place
    {
        get => _place;
        init
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(Place), "Place must have a positive value.");

            _place = value;
        }
    }

    public required SeatCategory Category { get; init; }
}

public enum SeatCategory
{
    Standart = 0,
    Premium = 1,
    VIP = 2
}
