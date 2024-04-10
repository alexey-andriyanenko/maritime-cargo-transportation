using Domain.Company.Entities;

namespace Infrastructure.Database.Entities;

public static class CompanyDbExtensions
{
    public static Company ToDomain(this CompanyDb company)
    {
        return new Company { Id = company.company_id, Name = company.company_name };
    }
}
