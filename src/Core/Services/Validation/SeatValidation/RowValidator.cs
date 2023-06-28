using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SeatValidation;
internal sealed class RowValidator : ChainValidator<Seat>
{
    private readonly ILogger? _logger;

    public RowValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Seat seat)
    {
        int row = seat.Row;

        if (row <= 1)
        {
            _logger?.LogError("A seat's row number must be a positive integer");
            return false;
        }

        if (row > 50)
        {
            _logger?.LogError("A seat's row number cannot be larger than 50");
            return false;
        }

        return true;
    }
}
