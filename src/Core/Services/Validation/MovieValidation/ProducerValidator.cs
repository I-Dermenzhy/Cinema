using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.MovieValidation;

internal sealed class ProducerValidator : ChainValidator<Movie>
{
    private const int MaxLength = 50;

    private readonly ILogger? _logger;

    public ProducerValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));

        string producer = movie.Producer;

        if (string.IsNullOrWhiteSpace(producer))
        {
            _logger?.LogError("Producer is required");
            return false;
        }

        if (producer.Length > MaxLength)
        {
            _logger?.LogError("A movie producer's name must consist of no more than {max length} symbols", MaxLength);
            return false;
        }

        return true;
    }
}
