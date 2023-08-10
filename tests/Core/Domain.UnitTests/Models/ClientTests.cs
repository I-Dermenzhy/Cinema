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
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void Constructor_InvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.That(() => new Client(email), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void Constructor_WithValidEmail_SetsEmailProperty()
    {
        string email = "test@example.com";

        var client = new Client(email);

        Assert.That(email, Is.EqualTo(client.Email));
    }

    [Test]
    public void AddTicket_TicketIsNull_ThrowsArgumentNullException()
    {
        Assert.That(() => _client.AddTicket(ticket: null!), Throws.ArgumentNullException);
    }

    [Test]
    public void AddTicket_WhenCalled_AddsPassedTicket()
    {
        Ticket ticket = new Mock<Ticket>().Object;

        _client.AddTicket(ticket);

        Assert.That(_client.Tickets.Contains(ticket));
    }

    [Test]
    public void RemoveTicket_WhenCalled_RemovesPassedTicket()
    {
        Ticket ticket = new Mock<Ticket>().Object;
        _client.AddTicket(ticket);

        _client.RemoveTicket(ticket);

        Assert.That(_client.Tickets, Does.Not.Contain(ticket));
    }
}