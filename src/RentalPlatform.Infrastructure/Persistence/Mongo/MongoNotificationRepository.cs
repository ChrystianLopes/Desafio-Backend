using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using RentalPlatform.Core.Entities;
using RentalPlatform.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RentalPlatform.Infrastructure.Persistence.Mongo;

public class MongoNotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notification> _notificationsCollection;

    public MongoNotificationRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _notificationsCollection = database.GetCollection<Notification>(settings.Value.CollectionName);
    }

    public async Task AddAsync(Notification notification, CancellationToken cancellationToken) =>
        await _notificationsCollection.InsertOneAsync(notification, null, cancellationToken);

    public async Task<IEnumerable<Notification>> GetAllAsync(CancellationToken cancellationToken) =>
        await _notificationsCollection.Find(_ => true).ToListAsync(cancellationToken);
}
