using System;

namespace RentalPlatform.Application.Contracts;

public record MotorcycleCreated(
    Guid Id,
    string Identifier,
    int Year,
    string LicensePlate
);

