using Domain.Company.Entities;

namespace Web.Companies.DTO;

public static class CompanyDTOExtensions
{
    public static CompanyResponse ToResponse(this Company company)
    {
        return new CompanyResponse
        {
            Id = company.Id,
            Name = company.Name,
            Email = company.Email,
            Phone = company.Phone,
            Address = company.Address,
            Website = company.Website
        };
    }
}
