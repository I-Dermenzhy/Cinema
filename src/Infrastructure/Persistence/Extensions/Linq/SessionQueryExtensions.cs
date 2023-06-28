using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions.Linq;

internal static class SessionQueryExtensions
{
    public static IQueryable<Session> ApplyFilters(this IQueryable<Session> query, SessionFilters filters)
    {
        if (filters.Date.HasValue)
            query = query.Where(s => DateOnly.FromDateTime(s.Duration.Start) == filters.Date);

        if (filters.Start.HasValue)
            query = query.Where(s => s.Duration.Start == filters.Start);

        if (filters.Movie is not null)
            query = query.Where(s => s.Movie == filters.Movie);

        if (filters.MovieId.HasValue)
            query = query.Where(s => s.MovieId == filters.MovieId);

        return query;
    }

    public static IQueryable<Session> IncludeNavigationProperties(this IQueryable<Session> query) =>
        query.Include(s => s.Movie);
}
