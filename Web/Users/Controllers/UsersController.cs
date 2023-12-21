using Domain.User.Entities;
using Domain.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("/users")]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<User>> GetUserEmailAsync([FromRoute] string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null) return NotFound();
        return Ok(user);
    }
}