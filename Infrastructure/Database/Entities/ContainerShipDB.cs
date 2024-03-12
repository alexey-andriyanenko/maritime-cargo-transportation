namespace Infrastructure.Database.Entities;

public class ContainerShipDB
{
    public int ShipId { get; set; }
    public int ContainerShipid { get; set; }
    public string ShipName { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public string CountryCountryCode { get; set; }
    public string CountryCode { get; set; }
    public int ShipTypeId { get; set; }
    public string ShipTypeName { get; set; }
    public int SizeTypeId { get; set; }
    public string SizeTypeName { get; set; }
    public int ContainerId { get; set; }
    public int ContainerTypeId { get; set; }
    public string ContainerTypeName { get; set; }
    public int CargoId { get; set; }
    public int CargoTypeId { get; set; }
    public string CargoTypeName { get; set; }
}