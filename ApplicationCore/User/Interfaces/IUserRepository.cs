using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.User.DTO;

namespace Domain.User.Interfaces;

public interface IUserRepository
{
    public Task<List<Entities.User>> GetListAsync();
    public Task<Entities.User?> GetByIdAsync(int id);
    public Task<Entities.User?> GetByEmailAsync(string email);
    public Task<int?> UpdateAsync(int id, UserUpdateRequest user);
    public Task<int?> CreateAsync(UserCreateRequest user);
}