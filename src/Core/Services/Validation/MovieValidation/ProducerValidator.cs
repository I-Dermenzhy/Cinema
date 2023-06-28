using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.MovieValidation;
internal sealed class ProducerValidator : ChainValidator<Movie>
{
    private readonly ILogger? _logger;

    public ProducerValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Movie movie)
    {
        string producer = movie.Producer;

        if (string.IsNullOrWhiteSpace(producer))
        {
            _logger?.LogError("Producer is required");
            return false;
        }

        if (producer.Length > 50)
        {
            _logger?.LogError("A movie producer's name must consist of no more than 60 symbols");
            return false;
        }

        return true;
    }
}
