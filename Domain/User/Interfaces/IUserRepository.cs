using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.User.Interfaces;

public interface IUserRepository
{
    public Task<List<Entities.User>> GetAllAsync();
    public Task<Entities.User?> GetByIdAsync(int id);
    public Task<Entities.User?> GetByEmailAsync(string email);
    public Task<int?> CreateAsync(Entities.User user);
}