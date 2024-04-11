using Domain.Ship.Entities;
using Web.Companies.DTO;
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
            Length = ship.Length,
            Beam = ship.Beam,
            Draft = ship.Draft,
            Imo = ship.Imo,
            YearBuilt = ship.YearBuilt,
            Country = ship.Country.ToResponse(baseUrl),
            Type = ship.Type,
            Company = ship.Company.ToResponse(),
        };
    }
}
