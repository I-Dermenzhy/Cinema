using Domain.Models;

namespace Domain.PriceEvalution;

public interface ITicketEvaluator<T> where T : Ticket
{
    decimal EvaluateCost(T ticket);
}

