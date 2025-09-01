using MediatR;
using System;
using System.Text.Json.Serialization;

namespace RentalPlatform.Application.Rentals.Commands.CreateRental;

public record CreateRentalCommand(
    [property: JsonPropertyName("entregador_id")] Guid DriverId,
    [property: JsonPropertyName("plano_dias")] int PlanDays
) : IRequest;