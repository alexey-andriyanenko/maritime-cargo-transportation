using System.Threading.Tasks;

namespace Domain.Company.Interfaces;

public interface ICompanyRepository
{
    public Task<Entities.Company> GetCompanyByIdAsync(int id);
    public Task<Entities.Company> GetCompanyByNameAsync(string name);
    public Task<int> CreateCompanyAsync(Entities.Company company);
}