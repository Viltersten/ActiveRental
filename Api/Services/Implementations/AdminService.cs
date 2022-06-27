using AutoMapper;

namespace Api.Services.Implementations;

public class AdminService : IAdminService
{
    public AdminService(IBillingService service, IMapper mapper, IRepository repo)
        => (Service, Mapper, Repo) = (service, mapper, repo);

    IBillingService Service { get; }
    IMapper Mapper { get; }
    IRepository Repo { get; }

    public async Task<PickupInfo> RegisterPickupAsync(PickupDto payload)
    {
        Rental rental = Mapper.Map<Rental>(payload);
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