using Domain.Cargo.Entities;
using Domain.Container.Entities;

namespace Web.ContainerShips.DTO;

public class ContainerWithinShip
{
    public int Id { get; set; }
    public Cargo Cargo { get; set; }
    public ContainerType Type { get; set; }
}