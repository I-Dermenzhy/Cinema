using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface IModelRepository<T, U> : IRepository<T>
    where T : IModel
    where U : IModelFilters<T>
{
    public Task<T> GetByIdAsync(Guid id);
    public Task<IEnumerable<T>> GetByFiltersAsync(U filters);

    public Task RemoveAsync(Guid id);
}
