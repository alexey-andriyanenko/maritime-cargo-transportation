using Domain.Cargo.Entities;

namespace Web.Cargoes.DTO;

public static class CargoDTOExtensions
{
    public static CargoResponse ToResponse(this Cargo cargo)
    {
        return new CargoResponse
        {
            Id = cargo.Id,
            CargoType = cargo.Type
        };
    }
}