using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.MovieValidation;

public sealed class MovieValidationChain : IValidationChain<Movie, MovieValidationChain>
{
    private readonly List<ChainValidator<Movie>> _validators = new();

    public MovieValidationChain AddValidator(ChainValidator<Movie> validator)
    {
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        _validators.Add(validator);
        return this;
    }

    public ChainValidator<Movie> Build() =>
        _validators.Aggregate((next, current) =>
        {
            current.SetNext(next);
            return current;
        });

    public ChainValidator<Movie> BuildDefault() =>
        new GenreValidator()
            .SetNext(new ProducerValidator()
                .SetNext(new TitleValidator()
            )
        );

    public ChainValidator<Movie> BuildDefault(ILogger logger) =>
        new GenreValidator(logger)
            .SetNext(new ProducerValidator(logger)
                .SetNext(new TitleValidator(logger)
            )
        );
}
