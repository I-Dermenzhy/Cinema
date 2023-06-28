using Domain.Models;

namespace Domain.Filters;

public class SeatFilters : IModelFilters<Seat>
{
    public int? Row { get; set; }
    public int? Place { get; set; }
    public SeatCategory? Category { get; set; }
}
