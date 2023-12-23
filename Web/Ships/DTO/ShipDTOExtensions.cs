using Domain.Ship.Entities;

namespace Web.Ships.DTO;

public static class ShipDTOExtensions
{
    public static ShipResponse ToResponse(this Ship ship)
    {
        return new ShipResponse
        {
            Id = ship.Id,
            Name = ship.Name,
            Flag = ship.Flag,
            Type = ship.Type,
            CompanyId = ship.Company.Id
        };
    }
}