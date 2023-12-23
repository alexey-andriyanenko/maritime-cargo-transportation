using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Company.DTO;

namespace Domain.Company.Interfaces;

public interface ICompanyRepository
{
    public Task<List<Entities.Company>> GetListAsync();
    public Task<Entities.Company?> GetByIdAsync(int id);
    public Task<Entities.Company?> GetByNameAsync(string name);
    public Task<int?> UpdateAsync(int id, CompanyUpdateRequest company);
    public Task<int?> CreateAsync(CompanyCreateRequest company);
}