using Domain.Country.Entities;
using Domain.Ship.Entities;
using Web.Countries.DTO;

namespace Web.Ships.DTO;

public class ShipResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public ShipType Type { get; set; }
    public CountryResponse Country { get; set; }
}