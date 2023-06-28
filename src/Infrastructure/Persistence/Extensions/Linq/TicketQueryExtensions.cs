using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Domain.Extensions.Linq;

internal static class TicketQueryExtensions
{
    public static IQueryable<Ticket> ApplyFilters(this IQueryable<Ticket> query, TicketFilters filters)
    {
        if (filters.Client is not null)
            query = query.Where(t => t.Client == filters.Client);

        if (filters.ClientId.HasValue)
            query = query.Where(t => t.ClientId == filters.ClientId);

        if (filters.Session is not null)
            query = query.Where(t => t.Session == filters.Session);

        if (filters.SessionId.HasValue)
            query = query.Where(t => t.SessionId == filters.SessionId);

        if (filters.Seat is not null)
            query = query.Where(t => t.Seat == filters.Seat);

        if (filters.SeatId.HasValue)
            query = query.Where(t => t.SeatId == filters.SeatId);

        if (filters.Discounts is not null && filters.Discounts.Count > 0)
            query = query.Where(t => t.Discounts.All(d => filters.Discounts.Contains(d)));

        return query;
    }

    public static IQueryable<Ticket> IncludeNavigationProperties(this IQueryable<Ticket> query) =>
        query.Include(t => t.Session)
                .ThenInclude(s => s.Movie)
            .Include(t => t.Seat)
            .Include(t => t.Discounts);
}
