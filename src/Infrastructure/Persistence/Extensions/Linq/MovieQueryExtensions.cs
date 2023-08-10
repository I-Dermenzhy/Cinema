using Domain.Filters;
using Domain.Models;

namespace Persistence.Extensions.Linq;

internal static class MovieQueryExtensions
{
    public static IQueryable<Movie> ApplyFilters(this IQueryable<Movie> query, MovieFilters filters)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNull(filters, nameof(filters));

        if (!string.IsNullOrEmpty(filters.Title))
            query = query.Where(m => m.Title == filters.Title);

        if (!string.IsNullOrEmpty(filters.Genre))
            query = query.Where(m => m.Genre == filters.Genre);

        if (!string.IsNullOrEmpty(filters.Producer))
            query = query.Where(m => m.Producer == filters.Producer);

        return query;
    }
}
