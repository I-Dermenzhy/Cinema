using Domain.Models;

using Microsoft.Extensions.Logging;

using System.Text.RegularExpressions;

namespace Services.Validation.ClientValidation;

internal sealed partial class EmailValidator : ChainValidator<Client>
{
    private const int MaxLength = 60;

    private readonly ILogger? _logger;

    public EmailValidator(ILogger? logger = null) => _logger = logger;

    public override bool Validate(Client client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        string email = client.Email;

        if (string.IsNullOrWhiteSpace(email))
        {
            _logger?.LogError("Email is required");
            return false;
        }

        if (email.Length > MaxLength)
        {
            _logger?.LogError("An email must consist of no more than {max length} symbols", MaxLength);
            return false;
        }

        if (!EmailRegex().IsMatch(email))
        {
            _logger?.LogError("An email must be in the format 'email@example.com'");
            return false;
        }

        return true;
    }

    [GeneratedRegex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailRegex();
}
