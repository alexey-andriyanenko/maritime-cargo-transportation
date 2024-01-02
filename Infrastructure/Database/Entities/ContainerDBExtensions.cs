using Domain.Cargo.Entities;
using Domain.Container.Entities;

namespace Infrastructure.Database.Entities;

public static class ContainerDBExtensions
{
    public static Container ToDomain(this ContainerDB model)
    {
        return new Container
        {
            Id = model.ContainerId,
            ContainerShipId = model.ContainerShipId,
            Type = new ContainerType
            {
                Id = model.ContainerTypeId,
                Name = model.ContainerTypeName
            },
            Cargo = new Cargo
            {
                Id = model.CargoTypeId,
                Type = new CargoType
                {
                    Id = model.CargoTypeId,
                    Name = model.CargoTypeName
                }
            }
        };
    }
}