namespace Api.Services.Implementations;

public class BillingService : IBillingService
{
    public BillingService(IOptionsSnapshot<BillingConfig> config, AppDbContext context)
        => (Config, Context) = (config.Value, context);

    BillingConfig Config { get; }
    AppDbContext Context { get; }

    public async Task<double> CalculateCostAsync(Rental rental)
    {
        Vehicle? vehicle = await Context.Vehicles.SingleOrDefaultAsync(a => a.Plate == rental.Plate);
        if (vehicle is null)
            throw new UnrecognizedVehicleException(rental.Plate);

        double duration = Math.Ceiling(rental.Duration.TotalDays);
        int distance = rental.Distance ?? 0;
        BillingConfig.Plan plan = Config.Plans[vehicle.Type.ToString()];
        
        double output = plan.OtherIncrement
          + duration * Config.DailyBaseCost * plan.DailyIncrement
          + distance * Config.RangeBaseCost * plan.RangeIncrement;

        return output;
    }
}