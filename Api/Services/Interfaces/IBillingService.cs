namespace Api.Services.Interfaces;

public interface IBillingService
{
    Task<double> CalculateCostAsync(Guid id, int mileage, bool finalize = true);
    Task<double> CalculateCostAsync(Rental rental, int mileage, bool finalize = true);
}