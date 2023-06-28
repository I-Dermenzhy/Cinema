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
        var duration = session.Duration;

        if (duration.Start >= duration.End)
        {
            _logger?.LogError($"The start time: {duration.Start} cannot be after (or equal to) the end time: {duration.End}");
            return false;
        }

        var startDate = DateOnly.FromDateTime(duration.Start);

        if (startDate < _minDateTime)
        {
            _logger?.LogError($"Only session from {_minDateTime.ToShortDateString()} are stored");
            return false;
        }

        if (startDate > _maxDateTime)
        {
            _logger?.LogError($"Only session up to {_maxDateTime.ToShortDateString()} are stored");
            return false;
        }

        return true;
    }
}
