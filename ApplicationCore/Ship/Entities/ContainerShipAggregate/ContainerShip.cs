﻿using System.Collections.Generic;

namespace Domain.Ship.Entities;

public class ContainerShip : Ship
{
    public ContainerShipSizeType SizeType { get; set; }
    public List<Container.Entities.Container> Containers { get; set; }
}