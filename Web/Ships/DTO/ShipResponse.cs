using Domain.Flag.Entities;
using Domain.Ship.Entities;

namespace Web.Ships.DTO;

public class ShipResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public ShipType Type { get; set; }
    public Flag Flag { get; set; }
}