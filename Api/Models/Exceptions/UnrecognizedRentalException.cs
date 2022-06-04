namespace Api.Models.Exceptions;

public class UnrecognizedRentalException : Exception
{
    public UnrecognizedRentalException(Guid id) => Id = id;

    Guid Id { get; }

    public override string Message
        => $"Rental {Id} not recognized in the system.";
}