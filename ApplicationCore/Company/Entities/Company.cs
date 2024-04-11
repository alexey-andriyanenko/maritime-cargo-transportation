using System.Collections.Generic;
using Domain.Shared.Entities;

namespace Domain.Company.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
    public List<Ship.Entities.Ship> Ships { get; set; }
}
