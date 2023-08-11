using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SessionValidation;

internal sealed class DurationValidator : ChainValidator<Session>
{
    private readonly ILogger? _logger;

    private readonly DateOnly _minDateTime = new(1970, 1, 1);
    private readonly DateOnly _maxDateTime = new(2025, 12, 31);

    public DurationValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Session session)
    {
        ArgumentNullException.ThrowIfNull(session, nameof(session));

        var duration = session.Duration;

        if (duration.Start >= duration.End)
        {
            _logger?.LogError("The start time: {start time} cannot be after (or equal to) the end time: {end time}", duration.Start, duration.End);
            return false;
        }

        var startDate = DateOnly.FromDateTime(duration.Start);

        if (startDate < _minDateTime)
        {
            _logger?.LogError("Only session from {min date} are stored", _minDateTime.ToShortDateString());
            return false;
        }

        if (startDate > _maxDateTime)
        {
            _logger?.LogError("Only session up to {max date} are stored", _maxDateTime.ToShortDateString());
            return false;
        }

        return true;
    }
}
