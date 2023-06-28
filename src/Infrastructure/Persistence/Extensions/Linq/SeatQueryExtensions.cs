using Domain.Filters;
using Domain.Models;

namespace Persistence.Extensions.Linq;

internal static class SeatQueryExtensions
{
    public static IQueryable<Seat> ApplyFilters(this IQueryable<Seat> query, SeatFilters filters)
    {
        if (filters.Row.HasValue)
            query = query.Where(s => s.Row == filters.Row);

        if (filters.Place.HasValue)
            query = query.Where(s => s.Place == filters.Place);

        if (filters.Category.HasValue)
            query = query.Where(s => s.Category == filters.Category);

        return query;
    }
}
