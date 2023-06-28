using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Movie : IModel
{
    public Movie() { }

    [SetsRequiredMembers]
    public Movie(string title, string genre, string producer)
    {
        Title = title;
        Genre = genre;
        Producer = producer;
    }

    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required string Producer { get; set; }
}
