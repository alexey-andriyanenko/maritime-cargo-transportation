﻿using Domain.Shared.Entities;

namespace Domain.Ship.Entities;

public class Ship : BaseEntity
{
    public string Name { get; set; }
    public Flag.Entities.Flag Flag { get; set; }
    public ShipType Type { get; set; }
    public Company.Entities.Company Company { get; set; }
}