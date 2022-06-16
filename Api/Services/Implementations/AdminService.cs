namespace Api.Services.Implementations;

public class AdminService : IAdminService
{
    public AdminService(IBillingService service, IRepository repo)
        => (Service, Repo) = (service, repo);

    IBillingService Service { get; }
    IRepository Repo { get; }

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
        await Repo.CreateRentalAsync(rental);

        PickupInfo output = new() { Id = rental.Id };

        return output;
    }

    public async Task<ReturnInfo> RegisterReturnAsync(ReturnDto payload)
    {
        double cost = await Service.CalculateCostAsync(payload.Id, payload.Mileage);

        ReturnInfo output = new() { Id = payload.Id, Charge = cost };

        return output;
    }

    public Task<Rental> GetRentalAsync(Guid id)
    {
        return Repo.GetRentalAsync(id);
    }

    public Task<Rental> GetRentalByPlateAsync(string plate)
    {
        return Repo.GetRentalByPlateAsync(plate);
    }
}