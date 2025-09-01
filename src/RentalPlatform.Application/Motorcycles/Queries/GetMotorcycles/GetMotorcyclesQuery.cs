using MediatR;

namespace RentalPlatform.Application.Motorcycles.Queries.GetMotorcycles;

public record GetMotorcyclesQuery(string? identificador) : IRequest<IEnumerable<MotorcycleDto>>;