using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.MovieValidation;

internal sealed class TitleValidator : ChainValidator<Movie>
{
    private readonly ILogger? _logger;

    public TitleValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));

        string title = movie.Title;

        if (string.IsNullOrWhiteSpace(title))
        {
            _logger?.LogError("Title is required");
            return false;
        }

        if (title.Length > 50)
        {
            _logger?.LogError("A movie title must consist of no more than 60 symbols");
            return false;
        }

        return true;
    }
}
