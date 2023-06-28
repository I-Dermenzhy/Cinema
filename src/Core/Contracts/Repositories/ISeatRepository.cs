using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface ISeatRepository : IModelRepository<Seat, SeatFilters>
{
}
