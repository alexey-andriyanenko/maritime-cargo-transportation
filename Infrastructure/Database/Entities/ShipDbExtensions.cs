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
            Length = model.ship_length,
            Beam = model.ship_beam,
            Draft = model.ship_draft,
            Imo = model.ship_imo,
            YearBuilt = model.ship_year_built,
            Country = new Country
            {
                Id = model.country_id,
                CountryCode = model.country_country_code,
                Code = model.country_code,
                Name = model.country_name
            },
            Company = new Company
            {
                Id = model.company_id,
                Name = model.company_name,
                Email = model.company_email,
                Phone = model.company_phone,
                Address = model.company_address,
                Website = model.company_website
            },
            Type = new ShipType { Id = model.ship_type_id, Name = model.ship_type_name },
        };
    }
}
