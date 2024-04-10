using Domain.Company.Entities;
using Domain.Country.Entities;
using Domain.Ship.Entities;

namespace Infrastructure.Database.Entities;

public static class ShipDbExtensions
{
    public static Ship ToDomain(this ShipDb model)
    {
        return new Ship
        {
            Id = model.ship_id,
            Name = model.ship_name,
            Country = new Country
            {
                Id = model.country_id,
                CountryCode = model.country_country_code,
                Code = model.country_code,
                Name = model.country_name
            },
            Type = new ShipType { Id = model.ship_type_id, Name = model.ship_type_name },
            Company = new Company { Id = model.company_id, Name = model.company_name }
        };
    }
}
