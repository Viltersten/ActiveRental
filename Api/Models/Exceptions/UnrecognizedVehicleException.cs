namespace Api.Models.Exceptions;

public class UnrecognizedVehicleException : Exception
{
    public UnrecognizedVehicleException(string plate) => Plate = plate;

    string Plate { get; }

    public override string Message
        => $"Vehicle {Plate} not recognized in the system.";
}