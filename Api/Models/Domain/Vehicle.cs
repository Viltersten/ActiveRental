namespace Api.Models.Domain;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public VehicleType Type { get; set; }
    public string Info { get; set; } = string.Empty;
}