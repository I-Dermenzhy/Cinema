using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Services.Validation.TicketValidation;
internal sealed class PriceValidator<T> : ChainValidator<T> where T : Ticket
{
    private readonly ILogger? _logger;

    private readonly decimal _maxPrice = 2000;

    public PriceValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(T ticket)
    {
        decimal price = ticket.Price;

        if (price <= 0)
        {
            _logger?.LogError("Price must be a positive number");
            return false;
        }

        if (price > _maxPrice)
        {
            _logger?.LogError($"Entered price exceeds the max allowed value: {_maxPrice}");
            return false;
        }

        return true;
    }
}
