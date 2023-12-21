using Domain.Shared.Entities;

namespace Domain.Cargo.Entities;

public class Cargo : BaseEntity
{
    public CargoType Type { get; set; }
}