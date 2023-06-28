using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Discounts;

using Moq;

using NUnit.Framework;

namespace Domain.UnitTests.Models;

[TestFixture]
public class TicketTests
{
    private Mock<Client> _clientMock;
    private Mock<Session> _sessionMock;
    private Mock<Seat> _seatMock;
    private Ticket _ticket;

    [SetUp]
    public void SetUp()
    {
        _clientMock = new Mock<Client>();
        _sessionMock = new Mock<Session>();
        _seatMock = new Mock<Seat>();
        _ticket = new Ticket(_clientMock.Object, _sessionMock.Object, _seatMock.Object, 10.0m);
    }

    [Test]
    public void AddDiscount_TotalDiscountExceeds1_ThrowsDiscountExceededException()
    {
        // Arrange
        Discount initialDiscount = new(0.8, "sample1");
        _ticket.AddDiscount(initialDiscount);

        Discount exceedingDiscount = new(0.4, "sample2");

        // Act and Assert
        Assert.That(() => _ticket.AddDiscount(exceedingDiscount), Throws.InstanceOf<DiscountExceededException>());
    }

    [Test]
    public void AddDiscount_TotalDiscountIsLessThan1_AddsDiscount()
    {
        // Arrange
        Discount initialDiscount = new(0.3, "sample1");
        _ticket.AddDiscount(initialDiscount);

        Discount newDiscount = new(0.4, "sample2");

        // Act
        _ticket.AddDiscount(newDiscount);

        // Assert
        Assert.That(_ticket.Discounts, Contains.Item(newDiscount));
    }

    [Test]
    public void RemoveDiscount_WhenCalled_RemovesExistingDiscount()
    {
        // Arrange
        var discountMock1 = new Mock<Discount>();
        var discountMock2 = new Mock<Discount>();

        _ticket.AddDiscount(discountMock1.Object);
        _ticket.AddDiscount(discountMock2.Object);

        // Act
        _ticket.RemoveDiscount(discountMock1.Object);

        // Assert
        Assert.That(_ticket.Discounts, Does.Not.Contain(discountMock1.Object));
        Assert.That(_ticket.Discounts, Contains.Item(discountMock2.Object));
    }
}