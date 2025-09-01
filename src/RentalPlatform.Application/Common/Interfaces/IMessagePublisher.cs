using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Common.Interfaces;

public interface IMessagePublisher
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class;
}