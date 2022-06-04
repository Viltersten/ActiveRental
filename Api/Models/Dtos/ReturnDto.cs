namespace Api.Models.Dtos;

public class ReturnDto
{
    public Guid Id { get; set; }
    // public Guid AgentId { get; set; }
    public DateTime Occasion { get; set; } = DateTime.Now;
    public int Mileage { get; set; }
    // todo Consider entity with remarks on inspection.
}