namespace Api.Services.Implementations;

public class AdminService : IAdminService
{
    public AdminService(IBillingService service, AppDbContext context)
        => (Service, Context) = (service, context);

    IBillingService Service { get; }
    AppDbContext Context { get; }

    public async Task<PickupInfo> RegisterPickupAsync(PickupDto payload)
    {
        Rental rental = new()
        {
            Plate = payload.Plate,
            PickupOn = payload.Occasion,
            CustomerId = payload.CustomerId,
            // DispacherId = payload.AgentId,
            Mileage = -payload.Mileage
        };
        await Context.Rentals.AddAsync(rental);
        await Context.SaveChangesAsync();

        PickupInfo output = new() { Id = rental.Id };

        return output;
    }

    public async Task<ReturnInfo> RegisterReturnAsync(ReturnDto payload)
    {
        Rental? rental = await Context.Rentals.SingleOrDefaultAsync(a => a.Id == payload.Id);
        if (rental is null)
            throw new UnrecognizedRentalException(payload.Id);
        if (rental is not { ReturnOn: null })
            throw new DuplicatedReturnException(payload.Id, payload.Occasion);

        rental.Mileage += payload.Mileage;
        rental.ReturnOn = DateTime.Now;
        rental.Credit = await Service.CalculateCostAsync(rental);

        await Context.SaveChangesAsync();

        ReturnInfo output = new() { Id = rental.Id, Charge = rental.Credit };

        return output;
    }
}