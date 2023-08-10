using System.Diagnostics.CodeAnalysis;

namespace Domain.Models.Discounts;

/// <summary>
/// Represents a discount applied to a price.
/// </summary>
public class Discount : IModel
{
    private double _value;

    public Discount()
    {
    }

    [SetsRequiredMembers]
    public Discount(double value, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description));

        Value = value;
        Description = description;
    }

    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the description of the discount.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the value of the discount. 
    /// The valid range is between 0 and 1, inclusive.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than 0 or greater than 1.</exception>
    /// <remarks>
    /// The discount value represents the portion of the price to subtract from the initial price.
    /// For example, if the initial price is 100 and the discount value is 0.15,
    /// the final price would be calculated as InitialPrice * (1 - Discount.Value) = 100 * (1 - 0.15) = 85.
    /// </remarks>
    public required double Value
    {
        get => _value;
        set
        {
            if (value is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _value = value;
        }
    }
}
