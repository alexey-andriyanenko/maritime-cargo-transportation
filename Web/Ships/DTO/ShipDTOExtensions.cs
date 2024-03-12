using Domain.Ship.Entities;
using Web.Countries.DTO;

namespace Web.Ships.DTO;

public static class ShipDTOExtensions
{
    public static ShipResponse ToResponse(this Ship ship, string baseUrl)
    {
        return new ShipResponse
        {
            Id = ship.Id,
            Name = ship.Name,
            Country = ship.Country.ToResponse(baseUrl),
            Type = ship.Type,
            CompanyId = ship.Company.Id
        };
    }
}