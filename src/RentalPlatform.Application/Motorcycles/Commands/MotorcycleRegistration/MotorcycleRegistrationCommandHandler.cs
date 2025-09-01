using MediatR;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Application.Common.Exceptions;
using RentalPlatform.Core.Entities;
using RentalPlatform.Application.Contracts;


namespace RentalPlatform.Application.Motorcycles.Commands.MotorcycleRegistration;

public class MotorcycleRegistrationCommandHandler : IRequestHandler<MotorcycleRegistrationCommand, string>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessagePublisher _publisher;

    public MotorcycleRegistrationCommandHandler(IMotorcycleRepository motorcycleRepository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
    {
        _motorcycleRepository = motorcycleRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

        public async Task<string> Handle(MotorcycleRegistrationCommand request, CancellationToken cancellationToken)
    {
        if (!await _motorcycleRepository.IsIdentifierUniqueAsync(request.Identifier, cancellationToken))
        {
            throw new InvalidOperationException("Já existe uma moto com este identificador.");
        }

        if (!await _motorcycleRepository.IsLicensePlateUniqueAsync(request.LicensePlate, cancellationToken))
        {
            throw new InvalidOperationException("Já existe uma moto com esta placa.");
        }

        var motorcycle = new Motorcycle(request.Identifier, request.Year, request.Model, request.LicensePlate);

        await _motorcycleRepository.AddAsync(motorcycle, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new Contracts.MotorcycleCreated(
            motorcycle.Id,
            motorcycle.Identifier,
            motorcycle.Year,
            motorcycle.LicensePlate), cancellationToken);

        return motorcycle.Identifier;
    }
}
