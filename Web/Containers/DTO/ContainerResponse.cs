using Domain.Cargo.Entities;
using Domain.Container.Entities;

namespace Web.Containers.DTO;

public class ContainerResponse
{
    public int Id { get; set; }
    public int ContainerShipId { get; set; }
    public ContainerType Type { get; set; }
    public Cargo Cargo { get; set; }
}