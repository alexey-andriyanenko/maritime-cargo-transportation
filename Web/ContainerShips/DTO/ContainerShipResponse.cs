using Domain.Country.Entities;
using Domain.Ship.Entities;
using Web.Companies.DTO;
using Web.Countries.DTO;

namespace Web.ContainerShips.DTO;

public class ContainerShipResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }
    public int Beam { get; set; }
    public int Draft { get; set; }
    public int Imo { get; set; }
    public int YearBuilt { get; set; }
    public CountryResponse Country { get; set; }
    public ShipType Type { get; set; }
    public ContainerShipSizeType SizeType { get; set; }
    public List<ContainerWithinShip> Containers { get; set; }
    public CompanyResponse Company { get; set; }
}
