using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface ISessionRepository : IModelRepository<Session, SessionFilters>
{
}
