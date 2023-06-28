using Domain.Models.Discounts;

namespace Domain.Filters;

public class DiscountFilters : IModelFilters<Discount>
{
    public string? Description { get; set; }
}
