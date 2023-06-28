namespace Contracts.Repositories;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAllAsync();

    public Task<Guid> InsertAsync(T model);
    public Task RemoveAsync(T model);
    public Task UpdateAsync(T model);
}
