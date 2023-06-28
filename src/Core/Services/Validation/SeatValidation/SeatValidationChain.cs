using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SeatValidation;
public sealed class SeatValidationChain : IValidationChain<Seat, SeatValidationChain>
{
    private readonly List<ChainValidator<Seat>> _validators = new();

    public SeatValidationChain AddValidator(ChainValidator<Seat> validator)
    {
        _validators.Add(validator);
        return this;
    }

    public ChainValidator<Seat> Build() =>
        _validators.Aggregate((next, current) =>
        {
            current.SetNext(next);
            return current;
        });

    public ChainValidator<Seat> BuildDefault() =>
        new PlaceValidator()
            .SetNext(new RowValidator());
    public ChainValidator<Seat> BuildDefault(ILogger logger) =>
        new PlaceValidator(logger)
            .SetNext(new RowValidator(logger));
}
