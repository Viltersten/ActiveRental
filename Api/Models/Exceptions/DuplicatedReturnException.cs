namespace Api.Models.Exceptions;

public class DuplicatedReturnException : Exception
{
    public DuplicatedReturnException(Guid id, DateTime? duplicate, DateTime occasion)
        => (Id, Duplicate, Occasion) = (id, duplicate, occasion);

    Guid Id { get; }
    DateTime? Duplicate { get; }
    DateTime Occasion { get; }

    public override string Message
        => $"Rental {Id} registered as returned on {Occasion.ToString(Patterns.Occasion)} already.";
}