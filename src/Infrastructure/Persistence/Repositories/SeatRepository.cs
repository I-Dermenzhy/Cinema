using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;
using Persistence.Extensions.Linq;

namespace Persistence.Repositories;

public sealed class SeatRepository : ISeatRepository
{
    private readonly CinemaDbContext _dbContext;

    public SeatRepository(CinemaDbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IEnumerable<Seat>> GetAllAsync() =>
        await _dbContext.Seats.ToListAsync();

    public async Task<Seat> GetByIdAsync(Guid id) =>
        await _dbContext.Seats.FindAsync(id) ??
        throw new ModelNotFoundException<Seat>(id);

    public async Task<IEnumerable<Seat>> GetByFiltersAsync(SeatFilters filters) =>
        await _dbContext.Seats
            .ApplyFilters(filters)
            .ToListAsync();

    public async Task<Guid> InsertAsync(Seat seat)
    {
        ArgumentNullException.ThrowIfNull(seat, nameof(seat));

        _dbContext.Seats.Add(seat);
        await _dbContext.SaveChangesAsync();

        return seat.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var seat = await _dbContext.Seats.FindAsync(id);

        if (seat is not null)
            await RemoveAsync(seat);
    }

    public async Task RemoveAsync(Seat seat)
    {
        ArgumentNullException.ThrowIfNull(seat, nameof(seat));

        _dbContext.Seats.Remove(seat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seat seat)
    {
        ArgumentNullException.ThrowIfNull(seat, nameof(seat));

        if (!_dbContext.Seats.Contains(seat))
            throw new ModelNotFoundException<Seat>(seat);

        await _dbContext.SaveChangesAsync();
    }
}
