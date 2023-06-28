using Domain.Models.Discounts;

namespace Domain.Exceptions;

public class DiscountExceededException : InvalidOperationException
{
    public DiscountExceededException(IDiscountable modelWithDiscounts, Discount excessDiscount)
        : this($"The overall discount value of {modelWithDiscounts} after adding the new discount with the value of {excessDiscount.Value} exceeds the limit of 1.0",
              null!, modelWithDiscounts, excessDiscount)
    {
    }

    public DiscountExceededException(string message, IDiscountable modelWithDiscounts, Discount excessDiscount)
        : this(message, null!, modelWithDiscounts, excessDiscount)
    {
    }

    public DiscountExceededException(string message, Exception innerException, IDiscountable modelWithDiscounts, Discount excessDiscount)
        : base(message, innerException)
    {
        ModelWithDiscounts = modelWithDiscounts;
        ExcessDiscount = excessDiscount;
    }

    public IDiscountable ModelWithDiscounts { get; init; }
    public Discount ExcessDiscount { get; init; }
}
