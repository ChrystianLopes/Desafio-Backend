using MassTransit;
using Microsoft.Extensions.Logging;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Application.Contracts;
using RentalPlatform.Core.Entities;

namespace RentalPlatform.Infrastructure.Messaging.Consumers;

public class MotorcycleCreatedConsumer : IConsumer<MotorcycleCreated>
{
    private readonly ILogger<MotorcycleCreatedConsumer> _logger;
    private readonly INotificationRepository _notificationRepository;

    public MotorcycleCreatedConsumer(ILogger<MotorcycleCreatedConsumer> logger, INotificationRepository notificationRepository)
    {
        _logger = logger;
        _notificationRepository = notificationRepository;
    }

    public async Task Consume(ConsumeContext<MotorcycleCreated> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received MotorcycleCreated event for Identifier: {Identifier}", message.Identifier);

        if (message.Year == 2024)
        {
            _logger.LogInformation("Processing notification for motorcycle from 2024.");
            var notification = new Notification(
                message.Id,
                message.LicensePlate,
                DateTime.UtcNow
            );
            await _notificationRepository.AddAsync(notification, context.CancellationToken);
            _logger.LogInformation("Notification for motorcycle {Identifier} processed successfully.", message.Identifier);
        }
    }
}

