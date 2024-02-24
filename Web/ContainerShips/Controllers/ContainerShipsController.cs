﻿using Domain.Ship.DTO;
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
        var ships = await _containerShipRepository.GetListAsync();
        var result = ships.Select(s => s.ToResponse());

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateContainerShip([FromBody] ContainerShipCreateRequest shipRequest)
    {
        var result = await _containerShipRepository.CreateAsync(shipRequest);

        if (result == null) return BadRequest();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> AppendContainerToContainerShip([FromRoute] int id,
        [FromQuery] int[] containers)
    {
        var result = await _containerShipRepository.AppendContainersAsync(id, containers);

        if (result == null) return BadRequest();
        return Ok();
    }
}