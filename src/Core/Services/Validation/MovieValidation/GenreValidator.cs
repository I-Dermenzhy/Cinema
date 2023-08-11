using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.MovieValidation;

internal sealed class GenreValidator : ChainValidator<Movie>
{
    private const int MaxLength = 50;

    private readonly List<string> _movieGenres = new()
    {
        "Action", "Adventure", "Animation", "Comedy", "Crime", "Documentary", "Drama",
        "Family", "Fantasy", "Horror", "Musical", "Mystery", "Romance", "Science Fiction",
        "Thriller", "War", "Western"
    };

    private readonly ILogger? _logger;

    public GenreValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));

        string genre = movie.Genre;

        if (string.IsNullOrWhiteSpace(genre))
        {
            _logger?.LogError("Genre is required");
            return false;
        }

        if (genre.Length > MaxLength)
        {
            _logger?.LogError("A movie genre must consist of no more than {max length} symbols", MaxLength);
            return false;
        }

        if (!_movieGenres.Contains(genre))
        {
            _logger?.LogError("A movie genre: {genre} was not recognized", genre);
            return false;
        }

        return true;
    }
}
