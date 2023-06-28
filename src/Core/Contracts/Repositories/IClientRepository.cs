using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface IClientRepository : IModelRepository<Client, ClientFilters>
{
}
