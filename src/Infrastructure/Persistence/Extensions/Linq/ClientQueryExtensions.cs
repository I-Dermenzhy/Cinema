using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions.Linq;

internal static class ClientQueryExtensions
{
    public static IQueryable<Client> ApplyFilters(this IQueryable<Client> query, ClientFilters filters)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNull(filters, nameof(filters));

        if (!string.IsNullOrEmpty(filters.Email))
            query = query.Where(c => c.Email == filters.Email);

        return query;
    }

    public static IQueryable<Client> IncludeNavigationProperties(this IQueryable<Client> query)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        return query
            .Include(c => c.Tickets)
                .ThenInclude(t => t.Session)
                    .ThenInclude(s => s.Movie)
             .Include(c => c.Tickets)
                .ThenInclude(t => t.Discounts)
             .Include(c => c.Tickets)
                .ThenInclude(t => t.Seat);
    }
}
