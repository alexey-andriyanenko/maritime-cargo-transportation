using Domain.Country.Entities;

namespace Web.Countries.DTO;

public static class CountryDTOExtensions
{
    public static CountryResponse ToResponse(this Country country, string baseUrl)
    {
        return new CountryResponse
        {
            Id = country.Id,
            Name = country.Name,
            CountryCode = country.CountryCode,
            FlagUrl = $"{baseUrl}/images/flags/{country.Code.ToLower()}.png"
        };
    }
}