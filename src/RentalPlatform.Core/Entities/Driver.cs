namespace RentalPlatform.Core.Entities;

public class Driver : BaseEntity
{
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string CnhNumber { get; private set; }
    public CnhType CnhType { get; private set; }
    public string? CnhImageUrl { get; private set; }

    private Driver() 
    {
        Name = string.Empty;
        Cnpj = string.Empty;
        CnhNumber = string.Empty;
    }

    public Driver(string name, string cnpj, DateTime birthDate, string cnhNumber, CnhType cnhType)
    {
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
        CnhNumber = cnhNumber;
        CnhType = cnhType;
    }

    public void UpdateCnhImage(string imageUrl)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl))
            CnhImageUrl = imageUrl;
    }
}