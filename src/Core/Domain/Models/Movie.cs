using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Movie : IModel
{
    public Movie()
    {
    }

    [SetsRequiredMembers]
    public Movie(string title, string genre, string producer)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));

        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException($"'{nameof(genre)}' cannot be null or whitespace.", nameof(genre));

        if (string.IsNullOrWhiteSpace(producer))
            throw new ArgumentException($"'{nameof(producer)}' cannot be null or whitespace.", nameof(producer));

        Title = title;
        Genre = genre;
        Producer = producer;
    }

    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Genre { get; init; }
    public required string Producer { get; init; }
}
