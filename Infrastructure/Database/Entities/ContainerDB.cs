namespace Infrastructure.Database.Entities;

public class ContainerDB
{
    public int ContainerId { get; set; }
    public int ContainerTypeId { get; set; }
    public string ContainerTypeName { get; set; }
    public int CargoId { get; set; }
    public int CargoTypeId { get; set; }
    public string CargoTypeName { get; set; }
    public int ContainerShipId { get; set; }
}