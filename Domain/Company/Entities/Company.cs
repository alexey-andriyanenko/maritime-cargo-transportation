using System.Collections.Generic;
using Domain.Shared.Entities;

namespace Domain.Company.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public List<Ship.Entities.Ship> Ships { get; set; }
}