using Domain.Models;

namespace Contracts.PriceEvalution;

public interface ITicketEvaluator
{
    decimal EvaluateCost(Ticket ticket);
}

public interface ITicketEvaluator<T> : ITicketEvaluator where T : Ticket
{
    decimal EvaluateCost(T ticket);
}
