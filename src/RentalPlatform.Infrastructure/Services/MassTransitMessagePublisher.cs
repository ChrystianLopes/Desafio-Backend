using MassTransit;
using RentalPlatform.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Infrastructure.Services;

public class MassTransitMessagePublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitMessagePublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class =>
        _publishEndpoint.Publish(message, cancellationToken);
}