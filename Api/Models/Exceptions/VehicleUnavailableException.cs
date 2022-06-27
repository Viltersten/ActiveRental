namespace Api.Models.Exceptions;

public class VehicleUnavailableException : Exception
{
    public VehicleUnavailableException(string plate) => Plate = plate;

    string Plate { get; }

    public override string Message
        => $"Vehicle {Plate} not available at the moment.";
}