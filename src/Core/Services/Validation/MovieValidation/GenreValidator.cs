using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.MovieValidation;

internal sealed class GenreValidator : ChainValidator<Movie>
{
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

        if (genre.Length > 50)
        {
            _logger?.LogError("A movie genre must consist of no more than 60 symbols");
            return false;
        }

        if (!_movieGenres.Contains(genre))
        {
            _logger?.LogError("A movie genre was not recognized");
            return false;
        }

        return true;
    }
}
