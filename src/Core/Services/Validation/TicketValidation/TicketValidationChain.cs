using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Services.Validation.TicketValidation;

public sealed class TicketValidationChain : IValidationChain<Ticket, TicketValidationChain>
{
    private readonly List<ChainValidator<Ticket>> _validators = new();

    public TicketValidationChain AddValidator(ChainValidator<Ticket> validator)
    {
        _validators.Add(validator);
        return this;
    }

    public ChainValidator<Ticket> Build() =>
        _validators.Aggregate((next, current) =>
        {
            current.SetNext(next);
            return current;
        });

    public ChainValidator<Ticket> BuildDefault() => new PriceValidator<Ticket>();
    public ChainValidator<Ticket> BuildDefault(ILogger logger) => new PriceValidator<Ticket>(logger);
}
