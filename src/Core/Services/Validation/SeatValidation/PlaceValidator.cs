using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SeatValidation;
internal sealed class PlaceValidator : ChainValidator<Seat>
{
    private readonly ILogger? _logger;

    public PlaceValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Seat seat)
    {
        int place = seat.Place;

        if (place <= 1)
        {
            _logger?.LogError("A seat's place number must be a positive integer");
            return false;
        }

        if (place > 50)
        {
            _logger?.LogError("A seat's place number cannot be larger than 50");
            return false;
        }

        return true;
    }
}
