using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SeatValidation;

internal sealed class PlaceValidator : ChainValidator<Seat>
{
    private const int MaxPlace = 50;

    private readonly ILogger? _logger;

    public PlaceValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Seat seat)
    {
        ArgumentNullException.ThrowIfNull(seat, nameof(seat));

        int place = seat.Place;

        if (place < 1)
        {
            _logger?.LogError("A seat's place number must be a positive integer, but was: {place}", place);
            return false;
        }

        if (place > MaxPlace)
        {
            _logger?.LogError("A seat's place number cannot be larger than {max place value}", MaxPlace);
            return false;
        }

        return true;
    }
}
