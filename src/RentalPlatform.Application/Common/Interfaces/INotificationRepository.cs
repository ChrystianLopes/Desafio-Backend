using RentalPlatform.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Common.Interfaces;

public interface INotificationRepository
{
    Task AddAsync(Notification notification, CancellationToken cancellationToken);
    Task<IEnumerable<Notification>> GetAllAsync(CancellationToken cancellationToken);
}
