using System.Threading.Tasks;

namespace Domain.Auth.Interfaces;

public interface IAuthService
{
    public Task Authenticate(User.Entities.User user);
}