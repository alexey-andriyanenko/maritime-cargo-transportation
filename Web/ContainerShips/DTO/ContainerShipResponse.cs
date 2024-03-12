using Domain.Country.Entities;
using Domain.Ship.Entities;

namespace Web.ContainerShips.DTO;

public class ContainerShipResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public ShipType Type { get; set; }
    public ContainerShipSizeType SizeType { get; set; }
    public List<ContainerWithinShip> Containers { get; set; }
}