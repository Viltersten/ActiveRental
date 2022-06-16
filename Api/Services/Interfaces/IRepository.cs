namespace Api.Services.Interfaces
{
    public interface IRepository
    {
        Task<Vehicle> GetVehicleAsync(Guid id);
        Task<Vehicle> GetVehicleByPlateAsync(string plate);
        Task<Vehicle[]> GetVehiclesAsync();

        Task<Rental> GetRentalAsync(Guid id);
        Task<Rental> GetRentalByPlateAsync(string plate);

        Task<Guid> CreateRentalAsync(Rental rental);
        Task<bool> UpdateRentalAsync(Guid id, DateTime occasion, int mileage, double debit);
    }
}
