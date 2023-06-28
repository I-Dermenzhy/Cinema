using Domain.Models;

namespace Domain.Filters;

public class SessionFilters : IModelFilters<Session>
{
    public DateOnly? Date { get; set; }
    public DateTime? Start { get; set; }

    public Movie? Movie { get; set; }
    public Guid? MovieId { get; set; }
}
