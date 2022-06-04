namespace Api.Models.Domain;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Plate { get; set; }
    public VehicleType Type { get; set; }
    public string Info { get; set; }
}