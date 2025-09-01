using RentalPlatform.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Common.Interfaces;

public interface IDriverRepository
{
    Task<Driver?> GetByIdAsync(Guid driverId, CancellationToken cancellationToken);
    Task AddAsync(Driver driver, CancellationToken cancellationToken);
    Task<bool> IsCnpjUniqueAsync(string cnpj, CancellationToken cancellationToken);
    Task<bool> IsCnhNumberUniqueAsync(string cnhNumber, CancellationToken cancellationToken);
}