﻿using Domain.Filters;
using Domain.Models.Discounts;

namespace Persistence.Extensions.Linq;

internal static class DiscountQueryExtensions
{
    public static IQueryable<Discount> ApplyFilters(this IQueryable<Discount> query, DiscountFilters filters)
    {
        if (!string.IsNullOrEmpty(filters.Description))
            query = query.Where(d => d.Description == filters.Description);

        return query;
    }
}
