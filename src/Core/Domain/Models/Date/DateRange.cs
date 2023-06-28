namespace Domain.Models.Date;

public class TimeRange :
    IEquatable<TimeRange>,
    ITerminatable<TimeRange, DateTime>,
    IClonable<TimeRange>
{
    public TimeRange(DateTime start, DateTime end)
    {
        if (start >= end)
            throw new ArgumentOutOfRangeException(nameof(end), "End value must be greater than start value.");

        Start = start;
        End = end;
    }

    public static explicit operator TimeRange((DateTime start, DateTime end) range) =>
       new(range.start, range.end);


    public static implicit operator (DateTime start, DateTime end)(TimeRange timeRange) =>
        (timeRange.Start, timeRange.End);

    public DateTime Start { get; }

    public DateTime End { get; }

    public override bool Equals(object? value) =>
           value is TimeRange r &&
           r.Start.Equals(Start) &&
           r.End.Equals(End);

    public bool Equals(TimeRange? other) =>
        other is not null && other.Start.Equals(Start) && other.End.Equals(End);

    public override int GetHashCode() => HashCode.Combine(Start, End);

    public TimeRange MoveStartTo(DateTime newStart) => new(newStart, End);

    public TimeRange MoveEndTo(DateTime newEnd) => new(Start, newEnd);

    public TimeRange Clone() => new(Start, End);
}
