namespace RentalPlatform.Core.Entities;

public class Rental : BaseEntity
{
    public Guid DriverId { get; private set; }
    public Guid MotorcycleId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime PredictedEndDate { get; private set; }
    public int RentalPlanDays { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Required for EF Core
    private Rental() { }

    public Rental(Guid driverId, Guid motorcycleId, DateTime startDate, int planDays, decimal dailyRate)
    {
        DriverId = driverId;
        MotorcycleId = motorcycleId;
        StartDate = startDate;
        PredictedEndDate = startDate.AddDays(planDays);
        EndDate = PredictedEndDate; // Initially, the end date is the predicted one
        RentalPlanDays = planDays;
        TotalCost = planDays * dailyRate;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdateReturn(DateTime actualReturnDate, decimal finalCost)
    {
        EndDate = actualReturnDate;
        TotalCost = finalCost;
    }
}