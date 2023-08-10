using Domain.Models;

using Moq;

using NUnit.Framework;

namespace Domain.UnitTests.Models;

[TestFixture]
public sealed class SessionSeatTests
{
    private Mock<Seat> _seatMock;
    private Mock<Session> _sessionMock;
    private SessionSeat _sessionSeat;

    [SetUp]
    public void SetUp()
    {
        _seatMock = new Mock<Seat>();
        _sessionMock = new Mock<Session>();
        _sessionSeat = new SessionSeat(_seatMock.Object, _sessionMock.Object);
    }

    [Test]
    public void IsBooked_SetToDifferentValue_InvokesOnBookingChanged()
    {
        var invoked = false;

        _sessionSeat.OnBookingChanged += (sender, value) => invoked = true;

        _sessionSeat.IsBooked = true;

        Assert.That(invoked, Is.True);
    }

    [Test]
    public void IsBooked_SetToSameValue_DoesNotInvokeOnBookingChanged()
    {
        var invoked = false;

        _sessionSeat.OnBookingChanged += (sender, value) => invoked = true;

        _sessionSeat.IsBooked = false;

        Assert.That(invoked, Is.False);
    }
}