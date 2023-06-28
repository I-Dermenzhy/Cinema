using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Seat : IModel
{
    public Seat() { }

    [SetsRequiredMembers]
    public Seat(int row, int place, SeatCategory category, bool isBooked)
    {
        Row = row;
        Place = place;
        Category = category;
    }

    public Guid Id { get; set; }
    public required int Row { get; set; }
    public required int Place { get; set; }
    public required SeatCategory Category { get; set; }
}

public enum SeatCategory
{
    Standart = 0,
    Premium = 1,
    VIP = 2
}
