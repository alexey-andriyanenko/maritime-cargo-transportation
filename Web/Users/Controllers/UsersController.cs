using Domain.User.DTO;
using Domain.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Users.DTO;

namespace Web.Controllers;

[Authorize]
[Route("/users")]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetListAsync();
        var response = users.Select(user => user.ToResponse()).ToList();

        return Ok(response);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync([FromRoute] int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) return NotFound();
        return Ok(user.ToResponse());
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserResponse>> GetUserEmailAsync([FromRoute] string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null) return NotFound();
        return Ok(user.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> UpdateAsync([FromRoute] int id, [FromBody] UserUpdateRequest userRequest)
    {
        var result = await _userRepository.UpdateAsync(id, userRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateUser([FromBody] UserCreateRequest userRequest)
    {
        var id = await _userRepository.CreateAsync(userRequest);

        if (id == null) return BadRequest();
        return Ok(id);
    }
}