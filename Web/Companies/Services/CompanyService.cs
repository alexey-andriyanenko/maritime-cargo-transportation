using Domain.Company.Entities;
using Domain.Company.Interfaces;

namespace Web.Companies.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyService(IHttpContextAccessor httpContextAccessor, ICompanyRepository companyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _companyRepository = companyRepository;
    }

    public async Task<Company?> GetByIdAsync(int companyId)
    {
        var userId = _httpContextAccessor.HttpContext.Session.GetInt32("user_id");
        if (userId == null) return null;

        Console.WriteLine($"USER_ID {userId}");

        // compiler doesn't understand statement above so i have to suppress it with ?? operator
        return await _companyRepository.GetByIdAsync(userId ?? 0, companyId);
    }
}