using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Auxiliaries;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        OnModelCreating(builder.Entity<Rental>());
    }

    private static void OnModelCreating(EntityTypeBuilder<Rental> entity)
    {
        // entity.HasIndex(a => new { a.Idp, a.ExternalId }).IsUnique();
        // entity.Property(a => a.Idp).HasMaxLength(64).IsRequired();
        // entity.Property(a => a.ExternalId).HasMaxLength(64).IsRequired();
        // entity.Property(a => a.FirstName).HasMaxLength(64).IsRequired();
        // entity.Property(a => a.LastName).HasMaxLength(64).IsRequired();
        // entity.Property(a => a.Label).IsRequired().HasDefaultValue(string.Empty);
        
        // entity.Ignore(a => a.FullName);
        // entity.HasKey(a => new { a.UserId, a.TenantId });
    }
}