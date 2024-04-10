using Domain.Ship.DTO;
using Domain.Ship.Enums;
using Domain.Ship.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ContainerShips.DTO;
using Web.Ships.DTO;

namespace Web.Controllers;

[Authorize]
[Route("/ships")]
public class ShipsController : Controller
{
    private readonly IShipRepository _shipRepository;

    public ShipsController(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ShipResponse>>> GetListAsync()
    {
        var companyId = HttpContext.Session.GetInt32("company_id") ?? 0;
        var ships = await _shipRepository.GetListAsync(companyId);
        var result = ships.Select(ship => ship.ToResponse(Request.Host.ToString()));

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShipResponse>> GetByIdAsync([FromRoute] int id)
    {
        var ship = await _shipRepository.GetByIdAsync(
            id,
            HttpContext.Session.GetInt32("company_id") ?? 0
        );

        if (ship == null)
            return NotFound();
        return Ok(ship.ToResponse(Request.Host.ToString()));
    }

    [HttpGet("details/{id:int}")]
    public async Task<ActionResult> GetDetailsAsync([FromRoute] int id)
    {
        var ship = await _shipRepository.GetByIdAsync(
            id,
            HttpContext.Session.GetInt32("company_id") ?? 0
        );
        if (ship == null)
            return NotFound();

        if (ship.Type.Id == (int)ShipTypesEnum.ContainerShip)
        {
            return Redirect($"/container-ships/{id}");
        }

        return NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> UpdateAsync(
        [FromRoute] int id,
        [FromBody] ShipUpdateRequest shipRequest
    )
    {
        var result = await _shipRepository.UpdateAsync(id, shipRequest);

        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAsync([FromBody] ShipCreateRequest shipRequest)
    {
        var result = await _shipRepository.CreateAsync(shipRequest);

        if (result == null)
            return BadRequest();
        return Ok(result);
    }
}
