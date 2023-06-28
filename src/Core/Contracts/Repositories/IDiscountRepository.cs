using Domain.Filters;
using Domain.Models.Discounts;

namespace Contracts.Repositories;

public interface IDiscountRepository : IModelRepository<Discount, DiscountFilters>
{
}
