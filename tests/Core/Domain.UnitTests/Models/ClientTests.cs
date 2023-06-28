using Domain.Models;

using Moq;

using NUnit.Framework;

namespace Domain.UnitTests.Models;

[TestFixture]
public sealed class ClientTests
{
    private Client _client;

    [SetUp]
    public void SetUp() => _client = new Mock<Client>().Object;

    [Test]
    public void AddTicket_TicketIsNull_ThrowsArgumentNullException()
    {
        // Act and Assert
        Assert.That(() => _client.AddTicket(null!), Throws.ArgumentNullException);
    }

    [Test]
    public void AddTicket_WhenCalled_AddsPassedTicket()
    {
        // Arrange
        Ticket ticket = new Mock<Ticket>().Object;

        // Act
        _client.AddTicket(ticket);

        // Assert
        Assert.That(_client.Tickets.Contains(ticket));
    }

    [Test]
    public void RemoveTicket_WhenCalled_RemovesPassedTicket()
    {
        // Arrange
        Ticket ticket = new Mock<Ticket>().Object;
        _client.AddTicket(ticket);

        // Act
        _client.RemoveTicket(ticket);

        // Assert
        Assert.That(_client.Tickets, Does.Not.Contain(ticket));
    }
}