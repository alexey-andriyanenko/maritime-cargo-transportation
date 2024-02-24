using Domain.Auth.Interfaces;
using Domain.Company.Interfaces;
using Domain.User.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Auth.DTO;

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

    [HttpPost("sign-in")]
    public async Task<ActionResult> SignInAsync([FromBody] SignInRequest request)
    {
        var user = await _userService.GetByEmailAsync(request.Email);

        if (user == null) return BadRequest();
        if (user.Password != request.Password) return Forbid();

        await _authService.Authenticate(user);

        return Ok();
    }

    [HttpPost("sign-in/company/{companyId:int}")]
    public async Task<ActionResult> SelectCompany([FromRoute] int companyId)
    {
        var company = await _companyService.GetByIdAsync(companyId);
        if (company == null) return BadRequest();

        HttpContext.Session.SetInt32("company_id", company.Id);

        return Ok();
    }

    [Authorize]
    [HttpPost("sign-out")]
    public async Task<ActionResult> SignOutAsync()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}