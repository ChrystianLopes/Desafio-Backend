using Microsoft.EntityFrameworkCore;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Core.Entities;

namespace RentalPlatform.Infrastructure.Persistence.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly RentalDbContext _context;

    public RentalRepository(RentalDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Rental rental, CancellationToken cancellationToken) =>
        await _context.Rentals.AddAsync(rental, cancellationToken);

    public async Task<Rental?> GetByIdAsync(Guid rentalId, CancellationToken cancellationToken) =>
        await _context.Rentals.FindAsync(new object[] { rentalId }, cancellationToken);

    public Task<bool> DriverHasActiveRentalAsync(Guid driverId, CancellationToken cancellationToken) =>
        _context.Rentals.AnyAsync(r => r.DriverId == driverId && r.EndDate >= DateTime.UtcNow.Date, cancellationToken);

    public Task<bool> HasRentalsForMotorcycleAsync(Guid motorcycleId, CancellationToken cancellationToken) =>
        _context.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId, cancellationToken);
}