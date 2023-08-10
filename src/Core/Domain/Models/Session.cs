using Domain.Models.Date;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Session : IModel
{
    public Session()
    {
    }

    [SetsRequiredMembers]
    public Session(Movie movie, TimeRange duration)
    {
        Movie = movie ?? throw new ArgumentNullException(nameof(movie));
        MovieId = movie.Id;
        Duration = duration ?? throw new ArgumentNullException(nameof(duration));
    }

    public Guid Id { get; init; }

    public required Movie Movie { get; init; }
    public required Guid MovieId { get; init; }

    public required TimeRange Duration { get; set; }
}

