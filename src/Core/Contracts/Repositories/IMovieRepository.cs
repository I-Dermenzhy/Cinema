using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface IMovieRepository : IModelRepository<Movie, MovieFilters>
{
}
