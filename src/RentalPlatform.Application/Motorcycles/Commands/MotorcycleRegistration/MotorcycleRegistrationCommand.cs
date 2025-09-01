using MediatR;
using System.Text.Json.Serialization;

namespace RentalPlatform.Application.Motorcycles.Commands.MotorcycleRegistration;

public record MotorcycleRegistrationCommand(
    [property: JsonPropertyName("identificador")] string Identifier,
    [property: JsonPropertyName("ano")] int Year,
    [property: JsonPropertyName("modelo")] string Model,
    [property: JsonPropertyName("placa")] string LicensePlate) : IRequest<string>;