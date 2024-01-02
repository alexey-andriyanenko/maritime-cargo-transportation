namespace Domain.Container.DTO;

public class ContainerCreateRequest
{
    public int ContainerShipId { get; set; }
    public int ContainerTypeId { get; set; }
    public int CargoId { get; set; }
}