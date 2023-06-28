using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions.Linq;

internal static class SessionSeatQueryExtensions
{
    public static IQueryable<SessionSeat> ApplyFilters(this IQueryable<SessionSeat> query, SessionSeatFilters filters)
    {
        query = query.ApplySeatFilters(filters.SeatFilters);
        query = query.ApplySessionFilters(filters.SessionFilters);

        if (filters.SeatId.HasValue)
            query = query.Where(ss => ss.SeatId == filters.SeatId);

        if (filters.SessionId.HasValue)
            query = query.Where(ss => ss.SessionId == filters.SessionId);

        if (filters.IsBooked.HasValue)
            query = query.Where(ss => ss.IsBooked == filters.IsBooked);

        return query;
    }

    public static IQueryable<SessionSeat> IncludeNavigationProperties(this IQueryable<SessionSeat> query) =>
        query.Include(ss => ss.Seat)
             .Include(ss => ss.Session)
                .ThenInclude(s => s.Movie);

    private static IQueryable<SessionSeat> ApplySeatFilters(this IQueryable<SessionSeat> query, SeatFilters? seatFilters)
    {
        if (seatFilters is null)
            return query;

        if (seatFilters.Row.HasValue)
            query = query.Where(ss => ss.Seat.Row == seatFilters.Row);

        if (seatFilters.Place.HasValue)
            query = query.Where(ss => ss.Seat.Place == seatFilters.Place);

        if (seatFilters.Category.HasValue)
            query = query.Where(ss => ss.Seat.Category == seatFilters.Category);

        return query;
    }

    private static IQueryable<SessionSeat> ApplySessionFilters(this IQueryable<SessionSeat> query, SessionFilters? sessionFilters)
    {
        if (sessionFilters is null)
            return query;

        if (sessionFilters.Date.HasValue)
            query = query.Where(ss => DateOnly.FromDateTime(ss.Session.Duration.Start) == sessionFilters.Date);

        if (sessionFilters.Start.HasValue)
            query = query.Where(ss => ss.Session.Duration.Start == sessionFilters.Start);

        if (sessionFilters.MovieId.HasValue)
            query = query.Where(ss => ss.Session.MovieId == sessionFilters.MovieId);

        if (sessionFilters.Movie is not null)
            query = query.Where(ss => ss.Session.Movie == sessionFilters.Movie);

        return query;
    }
}
