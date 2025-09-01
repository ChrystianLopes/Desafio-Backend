using MediatR;
using System;
using System.Text.Json.Serialization;

namespace RentalPlatform.Application.Drivers.Commands.CreateDriver;

public record CreateDriverCommand(
    [property: JsonPropertyName("nome")] string Name,
    [property: JsonPropertyName("cnpj")] string Cnpj,
    [property: JsonPropertyName("data_nascimento")] DateTime BirthDate,
    [property: JsonPropertyName("numero_cnh")] string CnhNumber,
    [property: JsonPropertyName("tipo_cnh")] string CnhType
) : IRequest;