using MediatR;
using RentalPlatform.Application.Motorcycles.Queries.GetMotorcycles;

namespace RentalPlatform.Application.Motorcycles.Queries.GetMotorcycleByIdentifier;

public record GetMotorcycleByIdentifierQuery(string Identifier) : IRequest<MotorcycleDto>;