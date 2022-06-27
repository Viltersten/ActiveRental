namespace Api.Services.Implementations;

public class BillingService : IBillingService
{
    public BillingService(IRepository repo, IOptionsSnapshot<BillingConfig> config)
        => (Config, Repo) = (config.Value, repo);

    BillingConfig Config { get; }
    IRepository Repo { get; }

    public async Task<double> CalculateCostAsync(Guid id, int mileage, bool finalize)
    {
        Rental rental = await Repo.GetRentalAsync(id);

        double output = await CalculateCostAsync(rental, mileage, finalize);

        return output;
    }

    public async Task<double> CalculateCostAsync(Rental rental, int mileage, bool finalize)
    {
        Vehicle vehicle = await Repo.GetVehicleByPlateAsync(rental.Plate);

        double duration = Math.Ceiling(rental.Duration.TotalDays);
        int distance = mileage + rental.Mileage;
        BillingConfig.Plan plan = Config.Plans[vehicle.Type.ToString()];
        double cost = plan.OtherIncrement
          + duration * Config.DailyBaseCost * plan.DailyIncrement
          + distance * Config.RangeBaseCost * plan.RangeIncrement;

        if (finalize)
            await Repo.UpdateRentalAsync(rental.Id, DateTime.Now, mileage, cost);

        return cost;
    }
}