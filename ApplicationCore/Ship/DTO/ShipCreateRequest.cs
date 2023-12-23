namespace Domain.Ship.DTO;

public class ShipCreateRequest
{
    public string Name { get; set; }
    public int FlagId { get; set; }
    public int ShipTypeId { get; set; }
    public int CompanyId { get; set; }
}