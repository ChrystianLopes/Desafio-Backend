using System.Text.Json.Serialization;

namespace RentalPlatform.Application.Motorcycles.Queries.GetMotorcycles;

public record MotorcycleDto(
    [property: JsonPropertyName("identificador")] string Identifier,
    [property: JsonPropertyName("ano")] int Year,
    [property: JsonPropertyName("modelo")] string Model,
    [property: JsonPropertyName("placa")] string LicensePlate);