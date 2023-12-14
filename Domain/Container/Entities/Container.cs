using Domain.Shared.Entities;

namespace Domain.Container.Entities;

public class Container : BaseEntity
{
    public ContainerType Type { get; set; }
    public Cargo.Entities.Cargo Cargo { get; set; }
}