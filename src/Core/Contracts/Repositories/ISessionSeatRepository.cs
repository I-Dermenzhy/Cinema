using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface ISessionSeatRepository : IModelRepository<SessionSeat, SessionSeatFilters>
{
}
