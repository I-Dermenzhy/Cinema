using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SeatValidation;

internal sealed class RowValidator : ChainValidator<Seat>
{
    private const int MaxPlace = 50;

    private readonly ILogger? _logger;

    public RowValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Seat seat)
    {
        ArgumentNullException.ThrowIfNull(seat, nameof(seat));

        int row = seat.Row;

        if (row < 1)
        {
            _logger?.LogError("A seat's row number must be a positive integer, but was: {place}", row);
            return false;
        }

        if (row > MaxPlace)
        {
            _logger?.LogError("A seat's row number cannot be larger than {max place value}", MaxPlace);
            return false;
        }

        return true;
    }
}
