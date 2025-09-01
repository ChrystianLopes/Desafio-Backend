using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RentalPlatform.Core.Entities;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    public Guid MotorcycleId { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime NotifiedAt { get; private set; }

    public Notification(Guid motorcycleId, string licensePlate, DateTime notifiedAt)
    {
        MotorcycleId = motorcycleId;
        LicensePlate = licensePlate;
        NotifiedAt = notifiedAt;
    }

    private Notification() { LicensePlate = string.Empty; }
}
