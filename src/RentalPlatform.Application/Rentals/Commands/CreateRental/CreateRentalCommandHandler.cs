using MediatR;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Rentals.Commands.CreateRental;

public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;

    private static readonly Dictionary<int, decimal> _rentalPlans = new()
    {
        { 7, 30.00m }, { 15, 28.00m }, { 30, 22.00m }, { 45, 20.00m }, { 50, 18.00m }
    };

    public CreateRentalCommandHandler(
        IDriverRepository driverRepository,
        IMotorcycleRepository motorcycleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork)
    {
        _driverRepository = driverRepository;
        _motorcycleRepository = motorcycleRepository;
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(request.DriverId, cancellationToken);
        if (driver is null)
            throw new InvalidOperationException("Entregador não encontrado.");

        if (driver.CnhType == CnhType.B)
            throw new InvalidOperationException("Entregador não possui CNH da categoria 'A'.");

        if (await _rentalRepository.DriverHasActiveRentalAsync(request.DriverId, cancellationToken))
            throw new InvalidOperationException("Entregador já possui uma locação ativa.");

        if (!_rentalPlans.TryGetValue(request.PlanDays, out var dailyRate))
            throw new InvalidOperationException("Plano de locação inválido. Planos disponíveis: 7, 15, 30, 45, 50 dias.");

        var motorcycle = await _motorcycleRepository.GetFirstAvailableAsync(cancellationToken);
        if (motorcycle is null)
            throw new InvalidOperationException("Nenhuma moto disponível para locação no momento.");

        var startDate = DateTime.UtcNow.Date.AddDays(1);
        var rental = new Rental(driver.Id, motorcycle.Id, startDate, request.PlanDays, dailyRate);

        await _rentalRepository.AddAsync(rental, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}