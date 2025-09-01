using MediatR;
using RentalPlatform.Application.Common.Exceptions;
using RentalPlatform.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Motorcycles.Commands.UpdateMotorcycleLicensePlate;

public class UpdateMotorcycleLicensePlateCommandHandler : IRequestHandler<UpdateMotorcycleLicensePlateCommand>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMotorcycleLicensePlateCommandHandler(IMotorcycleRepository motorcycleRepository, IUnitOfWork unitOfWork)
    {
        _motorcycleRepository = motorcycleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateMotorcycleLicensePlateCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetByIdentifierAsync(request.Identifier, cancellationToken);

        if (motorcycle is null)
        {
            throw new NotFoundException("Dados inválidos");
        }

        motorcycle.UpdateLicensePlate(request.NewLicensePlate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}