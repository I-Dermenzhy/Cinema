using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;
using Persistence.Extensions.Linq;

namespace Persistence.Repositories;

public sealed class ClientRepository : IClientRepository
{
    private readonly CinemaDbContext _dbContext;

    public ClientRepository(CinemaDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Client>> GetAllAsync() =>
        await _dbContext.Clients
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<Client> GetByIdAsync(Guid id) =>
        await _dbContext.Clients
            .IncludeNavigationProperties()
            .FirstOrDefaultAsync(c => c.Id == id) ??
        throw new ModelNotFoundException<Client>(id);

    public async Task<IEnumerable<Client>> GetByFiltersAsync(ClientFilters filters) =>
        await _dbContext.Clients
            .ApplyFilters(filters)
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<Guid> InsertAsync(Client client)
    {
        _dbContext.Clients.Add(client);

        await AddRelatedEntitiesAsync(client);

        await _dbContext.SaveChangesAsync();

        return client.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var client = await _dbContext.Clients.FindAsync(id);

        if (client is not null)
            await RemoveAsync(client);
    }

    public async Task RemoveAsync(Client client)
    {
        _dbContext.Clients.Remove(client);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        if (!_dbContext.Clients.Contains(client))
            throw new ModelNotFoundException<Client>(client);

        await AddRelatedEntitiesAsync(client);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRelatedEntitiesAsync(Client client)
    {
        foreach (var ticket in client.Tickets)
        {
            if (!_dbContext.Tickets.Contains(ticket))
                await _dbContext.Tickets.AddAsync(ticket);

            if (!_dbContext.Sessions.Contains(ticket.Session))
                await _dbContext.Sessions.AddAsync(ticket.Session);

            if (!_dbContext.Seats.Contains(ticket.Seat))
                await _dbContext.Seats.AddAsync(ticket.Seat);

            foreach (var discount in ticket.Discounts)
                if (!_dbContext.Discounts.Contains(discount))
                    await _dbContext.Discounts.AddAsync(discount);
        }
    }
}
