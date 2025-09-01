using Microsoft.EntityFrameworkCore;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Infrastructure.Persistence.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly RentalDbContext _context;

    public DriverRepository(RentalDbContext context)
    {
        _context = context;
    }

    public async Task<Driver?> GetByIdAsync(Guid driverId, CancellationToken cancellationToken) =>
        await _context.Drivers.FindAsync(new object[] { driverId }, cancellationToken);

    public async Task AddAsync(Driver driver, CancellationToken cancellationToken) =>
        await _context.Drivers.AddAsync(driver, cancellationToken);

    public async Task<bool> IsCnpjUniqueAsync(string cnpj, CancellationToken cancellationToken) =>
        !await _context.Drivers.AnyAsync(d => d.Cnpj == cnpj, cancellationToken);

    public async Task<bool> IsCnhNumberUniqueAsync(string cnhNumber, CancellationToken cancellationToken) =>
        !await _context.Drivers.AnyAsync(d => d.CnhNumber == cnhNumber, cancellationToken);
}