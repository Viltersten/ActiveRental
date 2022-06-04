using Api.Auxiliaries;
using Api.Models.Configs;
using Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Test;

public class BillingServiceTest
{
    [Fact]
    public async Task Charge01()
    {
        Dictionary<string, BillingConfig.Plan> plans = new()
        {
            {
                "sedan",
                new BillingConfig.Plan
                {
                    Type = VehicleType.Sedan,
                    DailyIncrement = 1.5,
                    RangeIncrement = 1.7,
                    OtherIncrement = 300
                }
            }
        };
        BillingConfig config = new() { DailyBaseCost = 1000, RangeBaseCost = 30, Plans = plans };
        Mock<IOptionsSnapshot<BillingConfig>> mockConfig = new();
        mockConfig.Setup(a => a.Value).Returns(mockConfig.Object.Value);
    
        Mock<DbSet<Rental>> mockDbSet = new();
        mockDbSet.As<IQueryable<Rental>>().Setup(a => a.Provider).Returns(MockRentals.Provider);
        mockDbSet.As<IQueryable<Rental>>().Setup(a => a.Expression).Returns(MockRentals.Expression);
        mockDbSet.As<IQueryable<Rental>>().Setup(a => a.ElementType).Returns(MockRentals.ElementType);
        mockDbSet.As<IQueryable<Rental>>().Setup(a => a.GetEnumerator()).Returns(MockRentals.GetEnumerator());
        Mock<AppDbContext> mockContext = new();
        mockContext.Setup(a => a.Rentals).Returns(mockDbSet.Object);
    
        BillingService service = new(mockConfig.Object, mockContext.Object);
    
        double result = await service.CalculateCostAsync(MockRentals.First());
    
        Mock<IBillingService> subject = new Mock<IBillingService>();
        subject.Setup(a => a.CalculateCostAsync(new Rental())).ReturnsAsync(3.14);
    }

    [Fact]
    public void Test03()
    {
        // IQueryable<Rental> data = new List<Rental> { new() { Plate = "abc" } }.AsQueryable();
        IQueryable<Rental> data = MockRentals.AsQueryable();

        Mock<DbSet<Rental>> mockSet = new();
        mockSet.As<IQueryable<Rental>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Rental>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Rental>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Rental>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        Mock<AppDbContext> mockContext = new Mock<AppDbContext>();
        mockContext.Setup(c => c.Rentals).Returns(mockSet.Object);

        BillingService service = new(
            new OptionsManager<BillingConfig>(null), 
            mockContext.Object);
    }


    // [Fact]
    // public void Charge02()
    // {
    //     Mock<IBillingService> subject = new Mock<IBillingService>();
    //     subject.Setup(a => a.CalculateCostAsync(new Rental())).ReturnsAsync(3.14);
    // }

    IQueryable<Rental> MockRentals => new List<Rental>
    {
        new()
        {
            PickupOn = new DateTime(2022, 4, 1, 10, 0, 0),
            ReturnOn = new DateTime(2022, 4, 1, 10, 0, 0),
            Plate = "abc123"
        }
    }.AsQueryable();

    static Vehicle[] MockVehicles = new[]
    {
        new Vehicle { Plate = "abc123", Type = VehicleType.Sedan }
    };
}

public class BloggingContext : DbContext
{
    public virtual DbSet<Blog> Blogs { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public virtual List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}