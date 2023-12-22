using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Company.Interfaces;

public interface ICompanyRepository
{
    public Task<List<Entities.Company>> GetAllAsync();
    public Task<Entities.Company?> GetByIdAsync(int id);
    public Task<Entities.Company?> GetByNameAsync(string name);
    public Task<int?> CreateAsync(Entities.Company company);
}