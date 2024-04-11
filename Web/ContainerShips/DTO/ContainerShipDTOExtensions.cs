using Domain.Ship.Entities;
using Web.Companies.DTO;
using Web.Countries.DTO;

namespace Web.ContainerShips.DTO;

public static class ContainerShipDTOExtensions
{
    public static ContainerShipResponse ToResponse(this ContainerShip model, string baseUrl)
    {
        return new ContainerShipResponse
        {
            Id = model.Id,
            Name = model.Name,
            Length = model.Length,
            Beam = model.Beam,
            Draft = model.Draft,
            Imo = model.Imo,
            YearBuilt = model.YearBuilt,
            Type = model.Type,
            Country = model.Country.ToResponse(baseUrl),
            Company = model.Company.ToResponse(),
            SizeType = model.SizeType,
            Containers = model
                .Containers.Select(c => new ContainerWithinShip
                {
                    Id = c.Id,
                    Cargo = c.Cargo,
                    Type = c.Type
                })
                .ToList()
        };
    }
}
