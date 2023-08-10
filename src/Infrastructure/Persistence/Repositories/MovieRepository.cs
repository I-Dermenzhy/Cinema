using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;
using Persistence.Extensions.Linq;

namespace Persistence.Repositories;

public sealed class MovieRepository : IMovieRepository
{
    private readonly CinemaDbContext _dbContext;

    public MovieRepository(CinemaDbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IEnumerable<Movie>> GetAllAsync() =>
    await _dbContext.Movies.ToListAsync();

    public async Task<Movie> GetByIdAsync(Guid id) =>
        await _dbContext.Movies.FindAsync(id) ??
        throw new ModelNotFoundException<Movie>(id);

    public async Task<IEnumerable<Movie>> GetByFiltersAsync(MovieFilters filters) =>
         await _dbContext.Movies
            .ApplyFilters(filters)
            .ToListAsync();

    public async Task<Guid> InsertAsync(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));

        _dbContext.Movies.Add(movie);
        await _dbContext.SaveChangesAsync();

        return movie.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var movie = await _dbContext.Movies.FindAsync(id);

        if (movie is not null)
            await RemoveAsync(movie);
    }

    public async Task RemoveAsync(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));

        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));

        if (!_dbContext.Movies.Contains(movie))
            throw new ModelNotFoundException<Movie>(movie);

        _dbContext.Movies.Update(movie);
        await _dbContext.SaveChangesAsync();
    }
}
