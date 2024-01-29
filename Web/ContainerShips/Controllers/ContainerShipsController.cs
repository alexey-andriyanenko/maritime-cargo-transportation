using Domain.Ship.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ContainerShips.DTO;

namespace Web.ContainerShips.Controllers;

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
        var ships = await _containerShipRepository.GetListAsync();
        var result = ships.Select(s => s.ToResponse());

        return Ok(result);
    }
}