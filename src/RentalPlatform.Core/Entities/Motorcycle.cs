namespace RentalPlatform.Core.Entities;

public class Motorcycle : BaseEntity
{
    public string Identifier { get; private set; }
    public int Year { get; private set; }
    public string Model { get; private set; }
    public string LicensePlate { get; private set; }

    private Motorcycle() 
    {
        Identifier = string.Empty;
        Model = string.Empty;
        LicensePlate = string.Empty;
    }

    public Motorcycle(string identifier, int year, string model, string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Identifier cannot be empty.", nameof(identifier));

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("O modelo do veículo não pode ser vazio.", nameof(model));

        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("A placa do veículo não pode ser vazia.", nameof(licensePlate));

        Identifier = identifier;
        Year = year;
        Model = model;
        LicensePlate = licensePlate;
    }

    public void UpdateLicensePlate(string newLicensePlate)
    {
        if (string.IsNullOrWhiteSpace(newLicensePlate))
            throw new ArgumentException("A nova placa não pode ser vazia.", nameof(newLicensePlate));
        LicensePlate = newLicensePlate;
    }
}