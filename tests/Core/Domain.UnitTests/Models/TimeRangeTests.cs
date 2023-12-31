﻿using Domain.Models.Date;

using NUnit.Framework;

namespace Domain.UnitTests.Models;

[TestFixture]
public sealed class TimeRangeTests
{
    [Test]
    public void Constructor_EndPrecedesStart_ThrowsArgumentException()
    {
        DateTime start = DateTime.MaxValue;
        DateTime end = DateTime.MinValue;

        Assert.That(() => new TimeRange(start, end), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void Constructor_EndEqualsStart_ThrowsArgumentException()
    {
        DateTime start = DateTime.MaxValue;
        DateTime end = start;

        Assert.That(() => new TimeRange(start, end), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void Equals_ObjectWithSameValues_ReturnsTrue()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;

        var timeRange1 = new TimeRange(start, end);
        var timeRange2 = new TimeRange(start, end);

        var result = timeRange1.Equals((object)timeRange2);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_ObjectWithDifferentValues_ReturnsFalse()
    {
        var timeRange1 = new TimeRange(DateTime.MinValue, DateTime.Now);
        var timeRange2 = new TimeRange(DateTime.MinValue, DateTime.MaxValue);

        var result = timeRange1.Equals((object)timeRange2);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Equals_SameTimeRange_ReturnsTrue()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;
        var timeRange = new TimeRange(start, end);

        var result = timeRange.Equals(timeRange);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_Null_ReturnsFalse()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;
        var timeRange = new TimeRange(start, end);

        var result = timeRange.Equals(null);

        Assert.That(result, Is.False);
    }

    [Test]
    public void GetHashCode_TwoEqualTimeRanges_ReturnsSameHashCode()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;

        var timeRange1 = new TimeRange(start, end);
        var timeRange2 = new TimeRange(start, end);

        var hashCode1 = timeRange1.GetHashCode();
        var hashCode2 = timeRange2.GetHashCode();

        Assert.That(hashCode2, Is.EqualTo(hashCode1));
    }

    [Test]
    public void GetHashCode_TwoDifferentTimeRanges_ReturnsDifferentHashCodes()
    {
        var timeRange1 = new TimeRange(DateTime.MinValue, DateTime.Now);
        var timeRange2 = new TimeRange(DateTime.MinValue, DateTime.MaxValue);

        var hashCode1 = timeRange1.GetHashCode();
        var hashCode2 = timeRange2.GetHashCode();

        Assert.That(hashCode2, Is.Not.EqualTo(hashCode1));
    }

    [Test]
    public void MoveStartTo_EndPrecedesStart_ThrowsArgumentException()
    {
        var start = DateTime.MinValue;
        var end = DateTime.Now;

        var timeRange = new TimeRange(start, end);
        var newStart = DateTime.MaxValue;

        Assert.That(() => timeRange.MoveStartTo(newStart), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void MoveStartTo_EndEqualsStart_ThrowsArgumentException()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;

        var timeRange = new TimeRange(start, end);
        var newStart = end;

        Assert.That(() => timeRange.MoveStartTo(newStart), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void MoveStartTo_ValidNewStart_ReturnsNewTimeRangeWithUpdatedStart()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;

        var timeRange = new TimeRange(start, end);
        var newStart = DateTime.Now;

        var movedTimeRange = timeRange.MoveStartTo(newStart);

        Assert.Multiple(() =>
        {
            Assert.That(movedTimeRange.Start, Is.EqualTo(newStart));
            Assert.That(movedTimeRange.End, Is.EqualTo(end));
        });
    }

    [Test]
    public void MoveEndTo_EndPrecedesStart_ThrowsArgumentException()
    {
        var start = DateTime.Now;
        var end = DateTime.MaxValue;

        var timeRange = new TimeRange(start, end);
        var newEnd = DateTime.MinValue;

        Assert.That(() => timeRange.MoveEndTo(newEnd), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void MoveEndTo_EndEqualsStart_ThrowsArgumentException()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;

        var timeRange = new TimeRange(start, end);
        var newEnd = start;

        Assert.That(() => timeRange.MoveEndTo(newEnd), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void MoveEndTo_ValidNewEnd_ReturnsNewTimeRangeWithUpdatedEnd()
    {
        var start = DateTime.Now;
        var end = DateTime.MaxValue;

        var timeRange = new TimeRange(start, end);
        var newEnd = DateTime.Now;

        var movedTimeRange = timeRange.MoveEndTo(newEnd);

        Assert.Multiple(() =>
        {
            Assert.That(movedTimeRange.Start, Is.EqualTo(start));
            Assert.That(movedTimeRange.End, Is.EqualTo(newEnd));
        });
    }

    [Test]
    public void Clone_WhenCalled_ReturnsNewTimeRangeWithSameValues()
    {
        var start = DateTime.MinValue;
        var end = DateTime.MaxValue;

        var timeRange = new TimeRange(start, end);

        var clonedTimeRange = timeRange.Clone();

        Assert.Multiple(() =>
        {
            Assert.That(clonedTimeRange.Start, Is.EqualTo(start));
            Assert.That(clonedTimeRange.End, Is.EqualTo(end));
            Assert.That(clonedTimeRange, Is.Not.SameAs(timeRange));
        });
    }
}
