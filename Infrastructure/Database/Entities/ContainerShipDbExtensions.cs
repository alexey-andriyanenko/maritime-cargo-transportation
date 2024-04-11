using Domain.Cargo.Entities;
using Domain.Company.Entities;
using Domain.Container.Entities;
using Domain.Country.Entities;
using Domain.Ship.Entities;

namespace Infrastructure.Database.Entities;

public static class ContainerShipDbExtensions
{
    public static ContainerShip ToDomain(
        this ContainerShipDb containerShipDb,
        IGrouping<int, ContainerShipDb> group
    )
    {
        var result = new ContainerShip
        {
            Id = containerShipDb.ship_id,
            Name = containerShipDb.ship_name,
            Length = containerShipDb.ship_length,
            Beam = containerShipDb.ship_beam,
            Draft = containerShipDb.ship_draft,
            Imo = containerShipDb.ship_imo,
            YearBuilt = containerShipDb.ship_year_built,
            Capacity = containerShipDb.container_ship_capacity,
            Country = new Country
            {
                Id = containerShipDb.country_id,
                Name = containerShipDb.country_name,
                CountryCode = containerShipDb.country_country_code,
                Code = containerShipDb.country_code
            },
            Company = new Company
            {
                Id = containerShipDb.company_id,
                Name = containerShipDb.company_name,
                Email = containerShipDb.company_email,
                Phone = containerShipDb.company_phone,
                Address = containerShipDb.company_address,
                Website = containerShipDb.company_website
            },
            ShipType = new ShipType
            {
                Id = containerShipDb.ship_type_id,
                Name = containerShipDb.ship_type_name
            },
            SizeType = new ContainerShipSizeType
            {
                Id = containerShipDb.size_type_id,
                Name = containerShipDb.size_type_name
            },
        };

        if (containerShipDb.container_id != 0)
        {
            result.Containers = group
                .Select(x => new Container
                {
                    Id = x.container_id,
                    ContainerShipId = x.container_ship_id,
                    Type = new ContainerType
                    {
                        Id = x.container_type_id,
                        Name = x.container_type_name
                    },
                    Cargo = new Cargo
                    {
                        Id = x.cargo_id,
                        Type = new CargoType { Id = x.cargo_type_id, Name = x.cargo_type_name }
                    }
                })
                .ToList();
        }
        else
        {
            result.Containers = new List<Container>();
        }

        return result;
    }
}
