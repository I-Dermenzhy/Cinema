namespace Domain.Models.Date;
public interface ITerminatable<TSelf, U>
    where TSelf : ITerminatable<TSelf, U>
    where U : IEquatable<U>
{
    public U Start { get; }

    public U End { get; }

    public TSelf MoveEndTo(U newEnd);

    public TSelf MoveStartTo(U newStart);
}
