using MediatR;
using RentalPlatform.Application.Common.Exceptions;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Application.Motorcycles.Queries.GetMotorcycles;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Motorcycles.Queries.GetMotorcycleByIdentifier;

public class GetMotorcycleByIdentifierQueryHandler : IRequestHandler<GetMotorcycleByIdentifierQuery, MotorcycleDto>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public GetMotorcycleByIdentifierQueryHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<MotorcycleDto> Handle(GetMotorcycleByIdentifierQuery request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetByIdentifierAsync(request.Identifier, cancellationToken);

        if (motorcycle is null)
        {
            throw new NotFoundException("Moto não encontrada com o identificador fornecido.");
        }

        return new MotorcycleDto(motorcycle.Identifier, motorcycle.Year, motorcycle.Model, motorcycle.LicensePlate);
    }
}