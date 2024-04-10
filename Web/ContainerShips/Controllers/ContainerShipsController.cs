using Domain.Ship.DTO;
using Domain.Ship.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ContainerShips.DTO;

namespace Web.ContainerShips.Controllers;

[Authorize]
[Route("/container-ships")]
public class ContainerShipsController : Controller
{
    private readonly IContainerShipRepository _containerShipRepository;

    public ContainerShipsController(IContainerShipRepository containerShipRepository)
    {
        _containerShipRepository = containerShipRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ContainerShipResponse>>> GetListAsync()
    {
        var ships = await _containerShipRepository.GetListAsync(
            HttpContext.Session.GetInt32("company_id") ?? 0
        );
        var result = ships.Select(s => s.ToResponse(Request.Host.ToString()));

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContainerShipResponse>> GetByIdAsync([FromRoute] int id)
    {
        var ship = await _containerShipRepository.GetByIdAsync(
            id,
            HttpContext.Session.GetInt32("company_id") ?? 0
        );

        if (ship == null)
            return NotFound();
        return Ok(ship.ToResponse(Request.Host.ToString()));
    }

    [HttpPost]
    public async Task<ActionResult> CreateContainerShip(
        [FromBody] ContainerShipCreateRequest shipRequest
    )
    {
        var result = await _containerShipRepository.CreateAsync(shipRequest);

        if (result == null)
            return BadRequest();
        return Ok();
    }
}
