using System.Security.Claims;
using Domain.Auth.Interfaces;
using Domain.User.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("email", user.Email)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await _httpContextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        _httpContextAccessor.HttpContext.Session.SetInt32("user_id", user.Id);
    }
}