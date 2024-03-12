using Domain.Country.Entities;

namespace Web.Countries.DTO;

public static class CountryDTOExtensions
{
    public static CountryResponse ToResponse(this Country country, string host)
    {
        return new CountryResponse
        {
            Id = country.Id,
            Name = country.Name,
            CountryCode = country.CountryCode,
            FlagUrl = $"{host}/images/flags/{country.Code.ToLower()}.png"
        };
    }
}