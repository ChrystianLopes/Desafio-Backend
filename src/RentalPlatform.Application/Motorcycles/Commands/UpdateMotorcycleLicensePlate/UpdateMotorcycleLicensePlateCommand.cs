using MediatR;
using System.Text.Json.Serialization;

namespace RentalPlatform.Application.Motorcycles.Commands.UpdateMotorcycleLicensePlate;

public record UpdateMotorcycleLicensePlateCommand : IRequest
{
    [JsonIgnore]
    public string Identifier { get; set; } = string.Empty;

    [JsonPropertyName("placa")]
    public string NewLicensePlate { get; init; } = string.Empty;
}