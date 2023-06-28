using Domain.Models;

namespace Domain.Filters;

public class MovieFilters : IModelFilters<Movie>
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? Producer { get; set; }
}
