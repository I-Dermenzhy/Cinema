using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;
using Persistence.Extensions.Linq;

namespace Persistence.Repositories;

public sealed class SessionRepository : ISessionRepository
{
    private readonly CinemaDbContext _dbContext;

    public SessionRepository(CinemaDbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IEnumerable<Session>> GetAllAsync() =>
        await _dbContext.Sessions
            .IncludeNavigationProperties()
            .ToListAsync();

    public async Task<Session> GetByIdAsync(Guid id) =>
        await _dbContext.Sessions
            .IncludeNavigationProperties()
            .FirstOrDefaultAsync(s => s.Id == id) ??
        throw new ModelNotFoundException<Session>(id);

    public async Task<IEnumerable<Session>> GetByFiltersAsync(SessionFilters filters) =>
       await _dbContext.Sessions
           .ApplyFilters(filters)
           .Include(s => s.Movie)
           .ToListAsync();

    public async Task<Guid> InsertAsync(Session session)
    {
        ArgumentNullException.ThrowIfNull(session, nameof(session));

        await _dbContext.Sessions.AddAsync(session);
        await AddRelatedEntitiesAsync(session);

        await _dbContext.SaveChangesAsync();

        return session.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var session = await _dbContext.Sessions.FindAsync(id);

        if (session is not null)
            await RemoveAsync(session);
    }

    public async Task RemoveAsync(Session session)
    {
        ArgumentNullException.ThrowIfNull(session, nameof(session));

        _dbContext.Sessions.Remove(session);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Session session)
    {
        ArgumentNullException.ThrowIfNull(session, nameof(session));

        if (!_dbContext.Sessions.Contains(session))
            throw new ModelNotFoundException<Session>(session);

        await AddRelatedEntitiesAsync(session);

        _dbContext.Sessions.Update(session);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRelatedEntitiesAsync(Session session)
    {
        if (!_dbContext.Movies.Contains(session.Movie))
            await _dbContext.Movies.AddAsync(session.Movie);
    }
}
