namespace Api.Models.Exceptions;

public class DuplicatedReturnException : Exception
{
    public DuplicatedReturnException(Guid id, DateTime occasion)
        => (Id, Occasion) = (id, occasion);

    Guid Id { get; }
    DateTime Occasion { get; }

    public override string Message
        => $"Rental {Id} registered as returned on {Occasion.ToString(Patterns.Occasion)} already.";
}