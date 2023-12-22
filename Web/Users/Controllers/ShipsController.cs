using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("/ships")]
public class ShipsController : Controller
{
    private readonly IShipRepository _shipRepository;

    public ShipsController(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Ship>>> GetListAsync()
    {
        var ships = await _shipRepository.GetListAsync();
        return Ok(ships);
    }

    [HttpGet("id/{id:int}")]
    public async Task<ActionResult<Ship>> GetByIdAsync([FromRoute] int id)
    {
        var ship = await _shipRepository.GetByIdAsync(id);

        if (ship == null) return NotFound();
        return Ok(ship);
    }
}