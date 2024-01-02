using Domain.Container.Entities;

namespace Web.Containers.DTO;

public static class ContainerDTOExtensions
{
    public static ContainerResponse ToResponse(this Container container)
    {
        return new ContainerResponse
        {
            Id = container.Id,
            Type = container.Type,
            ContainerShipId = container.ContainerShipId,
            Cargo = container.Cargo
        };
    }
}