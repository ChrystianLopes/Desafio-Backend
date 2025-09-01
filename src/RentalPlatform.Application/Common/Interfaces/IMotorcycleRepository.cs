using RentalPlatform.Core.Entities;

namespace RentalPlatform.Application.Common.Interfaces;

public interface IMotorcycleRepository
{
Task AddAsync(Motorcycle motorcycle, CancellationToken cancellationToken);
    Task<bool> IsIdentifierUniqueAsync(string identifier, CancellationToken cancellationToken);
    Task<bool> IsLicensePlateUniqueAsync(string licensePlate, CancellationToken cancellationToken);
    Task<IEnumerable<Motorcycle>> GetAllAsync(string? licensePlate, CancellationToken cancellationToken);
    Task<Motorcycle?> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken);
    void Delete(Motorcycle motorcycle);
    Task<Motorcycle?> GetFirstAvailableAsync(CancellationToken cancellationToken);
}