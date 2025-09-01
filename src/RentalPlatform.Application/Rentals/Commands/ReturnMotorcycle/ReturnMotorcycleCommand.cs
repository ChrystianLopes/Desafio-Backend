using MediatR;
using System;
using System.Text.Json.Serialization;

namespace RentalPlatform.Application.Rentals.Commands.ReturnMotorcycle;

public record ReturnMotorcycleCommand(
    [property: JsonPropertyName("locacao_id")] Guid RentalId,
    [property: JsonPropertyName("data_devolucao")] DateTime ReturnDate
) : IRequest<decimal>;