using Web.Companies.DTO;
using Web.Users.DTO;

namespace Web.Auth.DTO;

public class SessionResponse
{
    public UserResponse User { get; set; }
    public CompanyResponse? Company { get; set; }
}