using MediatR;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Drivers.Commands.CreateDriver;

public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverCommandHandler(IDriverRepository driverRepository, IUnitOfWork unitOfWork)
    {
        _driverRepository = driverRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        if (!await _driverRepository.IsCnpjUniqueAsync(request.Cnpj, cancellationToken))
        {
            throw new InvalidOperationException("CNPJ já cadastrado.");
        }

        if (!await _driverRepository.IsCnhNumberUniqueAsync(request.CnhNumber, cancellationToken))
        {
            throw new InvalidOperationException("Número da CNH já cadastrado.");
        }

        CnhType parsedCnhType = request.CnhType.ToUpper() switch
        {
            "A" => CnhType.A,
            "B" => CnhType.B,
            "A+B" => CnhType.AB,
            _ => throw new InvalidOperationException("Tipo de CNH inválido. Valores permitidos: A, B, A+B.")
        };

        var driver = new Driver(
            request.Name,
            request.Cnpj,
            request.BirthDate,
            request.CnhNumber,
            parsedCnhType);

        await _driverRepository.AddAsync(driver, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}