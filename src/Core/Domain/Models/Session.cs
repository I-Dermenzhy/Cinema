using Domain.Models.Date;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Session : IModel
{
    public Session() { }

    [SetsRequiredMembers]
    public Session(Movie movie, TimeRange duration)
    {
        Movie = movie;
        MovieId = movie.Id;
        Duration = duration;
    }

    public Guid Id { get; set; }

    public required Movie Movie { get; set; }
    public required Guid MovieId { get; set; }

    public required TimeRange Duration { get; set; }
}

