using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public class Client : IModel
{
    public Client()
    {
    }

    [SetsRequiredMembers]
    public Client(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));

        Email = email;
    }

    public Guid Id { get; init; }
    public required string Email { get; set; }
    public IList<Ticket> Tickets { get; init; } = new List<Ticket>();

    public void AddTicket(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));
        Tickets.Add(ticket);
    }

    public void RemoveTicket(Ticket ticket) => Tickets.Remove(ticket);
}
