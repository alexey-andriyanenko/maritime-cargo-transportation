using Domain.Cargo.Entities;
using Domain.Container.Entities;

namespace Infrastructure.Database.Entities;

public static class ContainerDbExtensions
{
    public static Container ToDomain(this ContainerDb model)
    {
        return new Container
        {
            Id = model.container_id,
            ContainerShipId = model.container_ship_id,
            Type = new ContainerType
            {
                Id = model.container_type_id,
                Name = model.container_type_name
            },
            Cargo = new Cargo
            {
                Id = model.cargo_id,
                Type = new CargoType { Id = model.cargo_type_id, Name = model.cargo_type_name }
            }
        };
    }
}
