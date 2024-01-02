using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<ContainerShip>>> GetListAsync()
    {
        var result = await _containerShipRepository.GetListAsync();
        return Ok(result);
    }
}