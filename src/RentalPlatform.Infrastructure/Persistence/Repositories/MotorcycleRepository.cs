using Microsoft.EntityFrameworkCore;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Core.Entities;

namespace RentalPlatform.Infrastructure.Persistence.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly RentalDbContext _context;

    public MotorcycleRepository(RentalDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Motorcycle motorcycle, CancellationToken cancellationToken) =>
        await _context.Motorcycles.AddAsync(motorcycle, cancellationToken);

    public async Task<bool> IsLicensePlateUniqueAsync(string licensePlate, CancellationToken cancellationToken) =>
        !await _context.Motorcycles.AnyAsync(m => m.LicensePlate == licensePlate, cancellationToken);

    public async Task<bool> IsIdentifierUniqueAsync(string identifier, CancellationToken cancellationToken) =>
        !await _context.Motorcycles.AnyAsync(m => m.Identifier == identifier, cancellationToken);

    public async Task<IEnumerable<Motorcycle>> GetAllAsync(string? licensePlate, CancellationToken cancellationToken)
    {
        var query = _context.Motorcycles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(licensePlate))
        {
            query = query.Where(m => m.LicensePlate == licensePlate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Motorcycle?> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken) =>
        await _context.Motorcycles.FirstOrDefaultAsync(m => m.Identifier == identifier, cancellationToken);

    public void Delete(Motorcycle motorcycle)
    {
        _context.Motorcycles.Remove(motorcycle);
    }

    public async Task<Motorcycle?> GetFirstAvailableAsync(CancellationToken cancellationToken)
    {
        var rentedMotorcycleIds = await _context.Rentals
            .Where(r => r.EndDate >= DateTime.UtcNow.Date) // Considera locações ativas ou futuras
            .Select(r => r.MotorcycleId)
            .Distinct()
            .ToListAsync(cancellationToken);

        return await _context.Motorcycles
            .FirstOrDefaultAsync(m => !rentedMotorcycleIds.Contains(m.Id), cancellationToken);
    }
}

