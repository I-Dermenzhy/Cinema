using Contracts.PriceEvalution;

using Domain.Models;
using Domain.Models.Discounts;

using Moq;

using NUnit.Framework;

using Services.PriceEvaluation;

namespace Services.UnitTests.PriceEvaluation;

[TestFixture]
public sealed class TicketEvaluatorTests
{
    private Mock<IEvaluationConfiguration> _configurationMock;
    private TicketEvaluator _ticketEvaluator;

    [SetUp]
    public void Setup()
    {
        _configurationMock = new Mock<IEvaluationConfiguration>();
        _ticketEvaluator = new TicketEvaluator(_configurationMock.Object);
    }

    [Test]
    public void Constructor_WithNullConfiguration_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TicketEvaluator(configuration: null!));
    }

    [Test]
    public void EvaluateCost_NullTicket_ThrowsArgumentNullException() =>
        Assert.That(() => _ticketEvaluator.EvaluateCost(null!), Throws.ArgumentNullException);

    [Test]
    public void EvaluateCost_ValidTicket_ReturnCorrectCost()
    {
        var basePrice = 50;

        _configurationMock.Setup(cm => cm.BasePrice).Returns(basePrice);
        _configurationMock.Setup(cm => cm.)

        var client = new Mock<Client>().Object;
        var session = new Mock<Session>().Object;
        var seat = new Mock<Seat>().Object;

        Discount discount1 = new(0.3, "Test1");
        Discount discount2 = new(0.2, "Test2");

        double totalDiscount = discount1.Value + discount2.Value;

        decimal expectedResult = _configuration.BasePrice * (decimal)(1 - totalDiscount);

        Ticket ticket = new()
        {
            Client = client,
            ClientId = Guid.NewGuid(),
            Session = session,
            SessionId = Guid.NewGuid(),
            Seat = seat,
            SeatId = Guid.NewGuid(),
            Price = expectedResult
        };

        ticket.AddDiscount(discount1);
        ticket.AddDiscount(discount2);

        decimal result = _ticketEvaluator.EvaluateCost(ticket);

        Assert.That(result, Is.EqualTo(expectedResult));
        Assert.That(result, Is.EqualTo(ticket.Price));
    }
}
