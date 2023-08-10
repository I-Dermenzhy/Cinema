using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Extensions.Linq;
using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;

namespace Persistence.Repositories;

public sealed class TicketRepository : ITicketRepository
{
    private readonly CinemaDbContext _dbContext;

    public TicketRepository(CinemaDbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IEnumerable<Ticket>> GetAllAsync() =>
        await _dbContext.Tickets
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<IEnumerable<Guid>> GetAllIdsAsync() =>
        await _dbContext.Tickets
            .Select(ticket => ticket.Id)
            .ToListAsync();

    public async Task<Ticket> GetByIdAsync(Guid id) =>
        await _dbContext.Tickets
            .IncludeNavigationProperties()
            .FirstOrDefaultAsync(t => t.Id == id) ??
        throw new ModelNotFoundException<Ticket>(id);

    public async Task<IEnumerable<Ticket>> GetByFiltersAsync(TicketFilters filters) =>
        await _dbContext.Tickets
            .ApplyFilters(filters)
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<Guid> InsertAsync(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));

        await _dbContext.Tickets.AddAsync(ticket);
        await AddRelatedEntitiesAsync(ticket);

        await _dbContext.SaveChangesAsync();

        return ticket.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ticket = await _dbContext.Tickets.FindAsync(id);

        if (ticket is not null)
            await RemoveAsync(ticket);
    }

    public async Task RemoveAsync(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));

        _dbContext.Tickets.Remove(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));

        if (!_dbContext.Tickets.Contains(ticket))
            throw new ModelNotFoundException<Ticket>(ticket);

        await AddRelatedEntitiesAsync(ticket);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRelatedEntitiesAsync(Ticket ticket)
    {
        var client = await _dbContext.Clients
            .Where(client => client.Id == ticket.ClientId)
            .FirstAsync();

        if (!client.Tickets.Contains(ticket))
            client.AddTicket(ticket);

        if (!_dbContext.Sessions.Contains(ticket.Session))
            await _dbContext.Sessions.AddAsync(ticket.Session);

        if (!_dbContext.Movies.Contains(ticket.Session.Movie))
            await _dbContext.Movies.AddAsync(ticket.Session.Movie);

        if (!_dbContext.Seats.Contains(ticket.Seat))
            await _dbContext.Seats.AddAsync(ticket.Seat);

        foreach (var discount in ticket.Discounts)
            if (!_dbContext.Discounts.Contains(discount))
                await _dbContext.Discounts.AddAsync(discount);
    }
}