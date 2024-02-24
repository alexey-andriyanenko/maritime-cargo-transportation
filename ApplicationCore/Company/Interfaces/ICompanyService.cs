using System.Threading.Tasks;

namespace Domain.Company.Interfaces;

public interface ICompanyService
{
    public Task<Entities.Company?> GetByIdAsync(int id);
}