using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;
using Persistence.Extensions.Linq;

namespace Persistence.Repositories;

public sealed class SessionSeatRepository : IModelRepository<SessionSeat, SessionSeatFilters>
{
    private readonly CinemaDbContext _dbContext;

    public SessionSeatRepository(CinemaDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<SessionSeat>> GetAllAsync() =>
        await _dbContext.SessionSeats
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<SessionSeat> GetByIdAsync(Guid id) =>
        await _dbContext.SessionSeats
            .IncludeNavigationProperties()
            .FirstOrDefaultAsync(ss => ss.Id == id) ??
        throw new ModelNotFoundException<SessionSeat>(id);

    public async Task<IEnumerable<SessionSeat>> GetByFiltersAsync(SessionSeatFilters filters) =>
        await _dbContext.SessionSeats
            .ApplyFilters(filters)
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<Guid> InsertAsync(SessionSeat sessionSeat)
    {
        _dbContext.SessionSeats.Add(sessionSeat);

        await AddRelatedEntitiesAsync(sessionSeat);

        await _dbContext.SaveChangesAsync();

        return sessionSeat.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var sessionSeat = await _dbContext.SessionSeats.FindAsync(id);

        if (sessionSeat is not null)
            await RemoveAsync(sessionSeat);
    }

    public async Task RemoveAsync(SessionSeat sessionSeat)
    {
        _dbContext.SessionSeats.Remove(sessionSeat);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(SessionSeat sessionSeat)
    {
        if (!_dbContext.SessionSeats.Contains(sessionSeat))
            throw new ModelNotFoundException<SessionSeat>(sessionSeat);

        await AddRelatedEntitiesAsync(sessionSeat);

        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRelatedEntitiesAsync(SessionSeat sessionSeat)
    {
        if (!_dbContext.Sessions.Contains(sessionSeat.Session))
            await _dbContext.Sessions.AddAsync(sessionSeat.Session);

        if (!_dbContext.Movies.Contains(sessionSeat.Session.Movie))
            await _dbContext.Movies.AddAsync(sessionSeat.Session.Movie);

        if (!_dbContext.Seats.Contains(sessionSeat.Seat))
            await _dbContext.Seats.AddAsync(sessionSeat.Seat);
    }
}
