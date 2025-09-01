using MediatR;
using RentalPlatform.Application.Common.Exceptions;
using RentalPlatform.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Motorcycles.Commands.DeleteMotorcycle;

public class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleCommand>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IRentalRepository rentalRepository, IUnitOfWork unitOfWork)
    {
        _motorcycleRepository = motorcycleRepository;
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetByIdentifierAsync(request.Identifier, cancellationToken);

        if (motorcycle is null)
        {
            throw new InvalidOperationException("Moto não encontrada.");
        }

        if (await _rentalRepository.HasRentalsForMotorcycleAsync(motorcycle.Id, cancellationToken))
        {
            throw new InvalidOperationException("Esta moto não pode ser removida pois possui locações associadas.");
        }

        _motorcycleRepository.Delete(motorcycle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}