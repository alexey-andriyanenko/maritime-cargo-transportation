using Web.Companies.DTO;
using Web.Users.DTO;

namespace Web.Auth.DTO;

public record SessionResponse
{
    public UserResponse User { get; init; }
    public CompanyResponse? Company { get; init; }
}
