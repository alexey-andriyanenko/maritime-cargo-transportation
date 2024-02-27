using Domain.Auth.Interfaces;
using Domain.Company.Interfaces;
using Domain.User.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Auth.DTO;
using Web.Companies.DTO;
using Web.Users.DTO;

namespace Web.Auth.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService, ICompanyService companyService)
    {
        _authService = authService;
        _userService = userService;
        _companyService = companyService;
    }


    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var user = await _userService.GetByEmailAsync(request.Email);

        if (user == null) return BadRequest();
        if (user.Password != request.Password) return Forbid();

        await _authService.Authenticate(user);
        HttpContext.Session.SetInt32("user_id", user.Id);

        return Ok();
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        HttpContext.Session.Clear();

        return Ok();
    }

    [Authorize]
    [HttpPost("session")]
    public async Task<ActionResult> FulfillSessionAsync([FromBody] SessionRequest request)
    {
        var company = await _companyService.GetByIdAsync(request.CompanyId);
        if (company == null) return BadRequest();

        HttpContext.Session.SetInt32("company_id", company.Id);

        return Ok();
    }

    [Authorize]
    [HttpGet("session")]
    public async Task<ActionResult> GetSessionAsync()
    {
        var user = await _userService.GetByIdAsync(HttpContext.Session.GetInt32("user_id") ?? 0);
        if (user == null) return await LogoutAsync();

        var company = await _companyService.GetByIdAsync(HttpContext.Session.GetInt32("company_id") ?? 0);

        var response = new SessionResponse
        {
            User = user.ToResponse(),
            Company = company?.ToResponse()
        };

        return Ok(response);
    }
}