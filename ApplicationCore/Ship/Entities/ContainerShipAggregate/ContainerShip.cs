﻿using System.Collections.Generic;

namespace Domain.Ship.Entities;

public class ContainerShip : Ship
{
    public int ContainerShipId { get; set; }
    public int Capacity { get; set; }
    public ContainerShipSizeType SizeType { get; set; }
    public List<Container.Entities.Container> Containers { get; set; }
    public ShipType ShipType { get; set; }
}
