using RentalPlatform.Application.Common.Interfaces;

namespace RentalPlatform.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly RentalDbContext _context;

    public UnitOfWork(RentalDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}