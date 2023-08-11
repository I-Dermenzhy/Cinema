using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation.TicketValidation;

internal sealed class PriceValidator<T> : ChainValidator<T> where T : Ticket
{
    private const decimal MaxPrice = 2000;

    private readonly ILogger? _logger;

    public PriceValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(T ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));

        decimal price = ticket.Price;

        if (price <= 0)
        {
            _logger?.LogError("Price must have a positive value, but was: {price}", price);
            return false;
        }

        if (price > MaxPrice)
        {
            _logger?.LogError($"Entered price exceeds the max allowed value: {MaxPrice}");
            return false;
        }

        return true;
    }
}
