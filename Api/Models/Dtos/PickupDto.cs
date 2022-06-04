namespace Api.Models.Dtos;

public class PickupDto
{
    public string Plate { get; set; }
    public string CustomerId { get; set; }
    // public Guid AgentId { get; set; }
    public DateTime Occasion { get; set; }
    public int Mileage { get; set; }
}