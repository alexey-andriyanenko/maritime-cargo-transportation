using System.Collections.Generic;

namespace Domain.Ship.DTO;

public class ContainerShipCreateRequest
{
    public int SizeTypeId { get; set; }
    public List<int> ContainersIds { get; set; }
}