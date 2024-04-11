using Domain.Shared.Entities;

namespace Domain.Ship.Entities;

public class Ship : BaseEntity
{
    public string Name { get; set; }
    public int Length { get; set; }
    public int Beam { get; set; }
    public int Draft { get; set; }
    public int Imo { get; set; }
    public int YearBuilt { get; set; }
    public Country.Entities.Country Country { get; set; }
    public ShipType Type { get; set; }
    public Company.Entities.Company Company { get; set; }
}
