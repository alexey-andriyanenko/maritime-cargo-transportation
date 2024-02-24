using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Company.DTO;

namespace Domain.Company.Interfaces;

public interface ICompanyRepository
{
    public Task<List<Entities.Company>> GetListAsync(int userId);
    public Task<Entities.Company?> GetByIdAsync(int userId, int companyId);
    public Task<int?> UpdateAsync(int id, CompanyUpdateRequest company);
    public Task<int?> CreateAsync(CompanyCreateRequest company);
}