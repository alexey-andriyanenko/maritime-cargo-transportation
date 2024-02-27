using Domain.Flag.Entities;
using Domain.Ship.Entities;

namespace Web.ContainerShips.DTO;

public class ContainerShipResponse
{
    public int ShipId { get; set; }
    public int ContainerShipId { get; set; }
    public string Name { get; set; }
    public Flag Flag { get; set; }
    public ShipType Type { get; set; }
    public ContainerShipSizeType SizeType { get; set; }
    public List<ContainerWithinShip> Containers { get; set; }
}