using Microsoft.Extensions.Configuration;

namespace Services.Exceptions;

public class ConfigurationSectionNotFoundException : Exception
{
    public ConfigurationSectionNotFoundException(IConfiguration configuration, string message)
        : base(message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));

        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public ConfigurationSectionNotFoundException(IConfiguration configuration, string message, Exception innerException)
        : base(message, innerException)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));

        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; init; }
}
