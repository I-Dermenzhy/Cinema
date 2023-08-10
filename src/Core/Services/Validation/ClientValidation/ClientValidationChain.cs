using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.ClientValidation;

public sealed class ClientValidationChain : IValidationChain<Client, ClientValidationChain>
{
    private readonly List<ChainValidator<Client>> _validators = new();

    public ClientValidationChain AddValidator(ChainValidator<Client> validator)
    {
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        _validators.Add(validator);
        return this;
    }

    public ChainValidator<Client> Build() =>
        _validators.Aggregate((next, current) =>
        {
            current.SetNext(next);
            return current;
        });

    public ChainValidator<Client> BuildDefault() => new EmailValidator();
    public ChainValidator<Client> BuildDefault(ILogger logger) => new EmailValidator(logger);
}
