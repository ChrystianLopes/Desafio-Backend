using MediatR;

namespace RentalPlatform.Application.Motorcycles.Commands.DeleteMotorcycle;

public record DeleteMotorcycleCommand(string Identifier) : IRequest;