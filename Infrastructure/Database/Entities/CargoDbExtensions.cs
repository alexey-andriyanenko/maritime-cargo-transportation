using Domain.Cargo.Entities;

namespace Infrastructure.Database.Entities;

public static class CargoDbExtensions
{
    public static Cargo ToDomain(this CargoDb cargoDb)
    {
        return new Cargo
        {
            Id = cargoDb.cargo_id,
            Type = new CargoType { Id = cargoDb.cargo_type_id, Name = cargoDb.cargo_type_name }
        };
    }
}
