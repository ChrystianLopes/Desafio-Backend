using MediatR;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Rentals.Commands.ReturnMotorcycle;

public class ReturnMotorcycleCommandHandler : IRequestHandler<ReturnMotorcycleCommand, decimal>
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;

    private static readonly Dictionary<int, decimal> _rentalPlans = new()
    {
        { 7, 30.00m }, { 15, 28.00m }, { 30, 22.00m }, { 45, 20.00m }, { 50, 18.00m }
    };

    public ReturnMotorcycleCommandHandler(IRentalRepository rentalRepository, IUnitOfWork unitOfWork)
    {
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<decimal> Handle(ReturnMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(request.RentalId, cancellationToken);
        if (rental is null)
        {
            throw new InvalidOperationException("Locação não encontrada.");
        }

        var returnDate = request.ReturnDate.Date;
        var predictedEndDate = rental.PredictedEndDate.Date;
        var startDate = rental.StartDate.Date;
        var dailyRate = _rentalPlans[rental.RentalPlanDays];
        decimal finalCost;

        if (returnDate < predictedEndDate)
        {
            var daysUsed = (returnDate - startDate).Days;
            var unusedDays = (predictedEndDate - returnDate).Days;
            var costForUsedDays = daysUsed * dailyRate;
            decimal penalty = 0;

            if (rental.RentalPlanDays == 7) penalty = (unusedDays * dailyRate) * 0.20m;
            else if (rental.RentalPlanDays == 15) penalty = (unusedDays * dailyRate) * 0.40m;

            finalCost = costForUsedDays + penalty;
        }
        else if (returnDate > predictedEndDate)
        {
            var extraDays = (returnDate - predictedEndDate).Days;
            finalCost = rental.TotalCost + (extraDays * 50.00m);
        }
        else
        {
            finalCost = rental.TotalCost;
        }

        rental.UpdateReturn(request.ReturnDate, finalCost);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return finalCost;
    }
}