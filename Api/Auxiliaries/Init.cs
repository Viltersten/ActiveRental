namespace Api.Auxiliaries;

public static class Init
{
    internal static async Task Migrate(WebApplication app)
    {
        IServiceScope scope = app.Services.CreateScope();
        AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        context.Vehicles.RemoveRange(context.Vehicles);

        await context.Vehicles.AddRangeAsync(Vehicles);

        await context.SaveChangesAsync();
    }

    static Vehicle[] Vehicles => new[]
    {
        new Vehicle { Plate = "abc123", Type = VehicleType.Sedan },
        new Vehicle { Plate = "grr205", Type = VehicleType.Sedan },
        new Vehicle { Plate = "hrs335", Type = VehicleType.Sedan },
        new Vehicle { Plate = "pat471", Type = VehicleType.Sedan },
        new Vehicle { Plate = "bqr533", Type = VehicleType.Wagon },
        new Vehicle { Plate = "xap690", Type = VehicleType.Wagon },
        new Vehicle { Plate = "wtl707", Type = VehicleType.Wagon },
        new Vehicle { Plate = "pok878", Type = VehicleType.Truck },
        new Vehicle { Plate = "vcm901", Type = VehicleType.Truck },
    };
}