using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.SessionValidation;
public sealed class SessionValidationChain : IValidationChain<Session, SessionValidationChain>
{
    private readonly List<ChainValidator<Session>> _validators = new();

    public SessionValidationChain AddValidator(ChainValidator<Session> validator)
    {
        _validators.Add(validator);
        return this;
    }

    public ChainValidator<Session> Build() =>
        _validators.Aggregate((next, current) =>
        {
            current.SetNext(next);
            return current;
        });

    public ChainValidator<Session> BuildDefault() => new DurationValidator();
    public ChainValidator<Session> BuildDefault(ILogger logger) => new DurationValidator(logger);
}
