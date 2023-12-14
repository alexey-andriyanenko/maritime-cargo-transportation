using System.Threading.Tasks;

namespace Domain.User.Interfaces;

public interface IUserRepository
{
    public Task<Entities.User> GetUserByIdAsync(int id);
    public Task<Entities.User> GetUserByEmailAsync(string email);
    public Task<int> CreateUserAsync(Entities.User user);
}