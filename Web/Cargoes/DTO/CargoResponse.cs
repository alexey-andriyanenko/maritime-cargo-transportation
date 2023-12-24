using Domain.Cargo.Entities;

namespace Web.Cargoes.DTO;

public class CargoResponse
{
    public int Id { get; set; }
    public CargoType CargoType { get; set; }
}