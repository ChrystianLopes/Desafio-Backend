using Microsoft.EntityFrameworkCore;
using RentalPlatform.Core.Entities;

namespace RentalPlatform.Infrastructure.Persistence;

public class RentalDbContext : DbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
    {
    }

    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalDbContext).Assembly);

        modelBuilder.Entity<Motorcycle>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => m.Identifier).IsUnique();
            entity.HasIndex(m => m.LicensePlate).IsUnique();
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.HasIndex(d => d.Cnpj).IsUnique();
            entity.HasIndex(d => d.CnhNumber).IsUnique();
        });
    }
}
