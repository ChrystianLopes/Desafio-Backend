using System;
using System.Threading;
using System.Threading.Tasks;
using RentalPlatform.Core.Entities;

namespace RentalPlatform.Application.Common.Interfaces;

public interface IRentalRepository
{
    Task AddAsync(Rental rental, CancellationToken cancellationToken);
    Task<Rental?> GetByIdAsync(Guid rentalId, CancellationToken cancellationToken);
    Task<bool> DriverHasActiveRentalAsync(Guid driverId, CancellationToken cancellationToken);
    Task<bool> HasRentalsForMotorcycleAsync(Guid motorcycleId, CancellationToken cancellationToken);
}