using Domain.Models;

namespace Domain.Filters;

public class ClientFilters : IModelFilters<Client>
{
    public string? Email { get; set; }
}
