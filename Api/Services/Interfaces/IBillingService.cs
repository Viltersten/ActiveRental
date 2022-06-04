namespace Api.Services.Interfaces;

public interface IBillingService
{
    Task<double> CalculateCostAsync(Rental rental);
}