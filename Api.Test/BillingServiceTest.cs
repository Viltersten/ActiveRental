namespace Api.Test;

public class BillingServiceTest
{
    public BillingServiceTest()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.test.json", false)
            .Build();
        BillingConfig config = configuration.GetSection("Billing").Get<BillingConfig>();
        Config = Options.Create(config).Value;
    }

    BillingConfig Config { get; }

    [Fact]
    public async Task OneOfManyTestCases()
    {
        Vehicle vehicle = MockVehicles[0];
        Rental rental = MockRentals[0];
        Mock<IRepository> repo = new();
        repo.Setup(a => a.GetVehicleByPlateAsync(vehicle.Plate)).ReturnsAsync(vehicle);
        repo.Setup(a => a.GetRentalByPlateAsync(vehicle.Plate)).ReturnsAsync(rental);
        Mock<IOptionsSnapshot<BillingConfig>> config = new();
        config.Setup(a => a.Value).Returns(Config);

        double actual = await new BillingService(repo.Object, config.Object)
            .CalculateCostAsync(rental, rental.Mileage + 120, false);

        Assert.Equal(15300, actual);
    }

    static readonly Rental[] MockRentals =
    {
        new()
        {
            PickupOn = new DateTime(2022, 4, 1, 10, 0, 0),
            ReturnOn = new DateTime(2022, 4, 3, 11, 0, 0),
            Plate = "abc123",
            Mileage = 1000
        }
    };

    static readonly Vehicle[] MockVehicles =
    {
        new() { Plate = "abc123", Type = VehicleType.Sedan }
    };
}
