namespace Api.Models.Domain;

public class Rental
{
    public Guid Id { get; set; }
    public string Plate { get; set; }
    public DateTime PickupOn { get; set; }
    public DateTime? ReturnOn { get; set; }
    public string CustomerId { get; set; }
    // public Guid DispacherId { get; set; }
    // public Guid? RetrieverId { get; set; }
    public int Mileage { get; set; }
    public double Credit { get; set; }

    public int? Distance => Available ? Mileage : null;
    public TimeSpan Duration => (ReturnOn ?? DateTime.Now) - PickupOn;
    public bool Available => ReturnOn is not null;
}