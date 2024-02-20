using System.Collections.Generic;

namespace Domain.Ship.DTO;

public class ContainerShipCreateRequest : ShipCreateRequest
{
    public int SizeTypeId { get; set; }
}