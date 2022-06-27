namespace Api.Services.Implementations;

public class Repository : IRepository
{
    public Repository(AppDbContext context) => Context = context;

    AppDbContext Context { get; }

    public async Task<Vehicle> GetVehicleAsync(Guid id)
    {
        Vehicle? result = await Context.Vehicles.SingleOrDefaultAsync(a => a.Id == id);
        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    public async Task<Vehicle> GetVehicleByPlateAsync(string plate)
    {
        Vehicle? result = await Context.Vehicles.SingleOrDefaultAsync(a => a.Plate == plate);
        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    public async Task<Vehicle[]> GetVehiclesAsync()
    {
        Vehicle[] result = await Context.Vehicles.ToArrayAsync();

        return result;
    }

    public async Task<Rental> GetRentalAsync(Guid id)
    {
        Rental? result = await Context.Rentals.SingleOrDefaultAsync(a => a.Id == id);
        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    public async Task<Rental> GetRentalByPlateAsync(string plate)
    {
        Rental? result = await Context.Rentals.SingleOrDefaultAsync(a => a.Plate == plate);
        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    public async Task<Guid> CreateRentalAsync(Rental rental)
    {
        bool unavailable = await Context.Rentals
            .AnyAsync(a => a.Plate == rental.Plate && a.ReturnOn == null);
        if (unavailable)
            throw new VehicleUnavailableException(rental.Plate);

        await Context.Rentals.AddAsync(rental);
        await Context.SaveChangesAsync();

        return rental.Id;
    }

    public async Task<bool> UpdateRentalAsync(Guid id, DateTime occasion, int mileage, double debit)
    {
        Rental? rental = await Context.Rentals.SingleOrDefaultAsync(a => a.Id == id);
        if (rental is null)
            throw new UnrecognizedRentalException(id);
        if (rental is not { ReturnOn: null })
            throw new DuplicatedReturnException(id, rental.ReturnOn, occasion);

        rental.Mileage += mileage;
        rental.ReturnOn = DateTime.Now;
        rental.Credit = debit;

        await Context.SaveChangesAsync();

        return true;
    }
}