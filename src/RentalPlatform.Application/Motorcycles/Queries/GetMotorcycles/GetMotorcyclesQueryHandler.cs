using MediatR;
using RentalPlatform.Application.Common.Exceptions;
using RentalPlatform.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Motorcycles.Queries.GetMotorcycles;

public class GetMotorcyclesQueryHandler : IRequestHandler<GetMotorcyclesQuery, IEnumerable<MotorcycleDto>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public GetMotorcyclesQueryHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<IEnumerable<MotorcycleDto>> Handle(GetMotorcyclesQuery request, CancellationToken cancellationToken)
    {
        var motorcycles = await _motorcycleRepository.GetAllAsync(request.identificador, cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.identificador) && !motorcycles.Any())
        {
            throw new NotFoundException("Veículo não localizado, verifique a placa.");
        }

        return motorcycles.Select(m => new MotorcycleDto(
            m.Identifier,
            m.Year,
            m.Model,
            m.LicensePlate
        ));
    }
}
