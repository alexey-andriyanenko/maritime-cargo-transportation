using Domain.Cargo.DTO;
using Domain.Cargo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Cargoes.DTO;

namespace Web.Cargoes.Controllers;

[Authorize]
[Route("/cargoes")]
public class CargoesController : Controller
{
    private readonly ICargoRepository _cargoRepository;

    public CargoesController(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CargoResponse>>> GetListAsync()
    {
        var cargoes = await _cargoRepository.GetListAsync();
        var result = cargoes.Select(cargo => cargo.ToResponse());

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CargoResponse>> GetByIdAsync([FromRoute] int id)
    {
        var cargo = await _cargoRepository.GetByIdAsync(id);

        if (cargo == null) return NotFound();
        return Ok(cargo);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> UpdateAsync([FromRoute] int id, [FromBody] CargoUpdateRequest cargoRequest)
    {
        var result = await _cargoRepository.UpdateAsync(id, cargoRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAsync([FromBody] CargoCreateRequest cargoRequest)
    {
        var result = await _cargoRepository.CreateAsync(cargoRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }
}