using System.Threading.Tasks;

namespace Domain.User.Interfaces;

public interface IUserService
{
    public Task<Entities.User?> GetByIdAsync(int id);
    public Task<Entities.User?> GetByEmailAsync(string email);
}