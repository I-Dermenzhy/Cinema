using Domain.Filters;
using Domain.Models;

namespace Contracts.Repositories;

public interface ITicketRepository : IModelRepository<Ticket, TicketFilters>
{
}
