using Domain.Container.DTO;
using Domain.Container.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Containers.DTO;

namespace Web.Containers.Controllers;

[Authorize]
[Route("/containers")]
public class ContainersController : Controller
{
    private readonly IContainerRepository _containerRepository;

    public ContainersController(IContainerRepository containerRepository)
    {
        _containerRepository = containerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ContainerResponse>>> GetListAsync()
    {
        var containers = await _containerRepository.GetListAsync();
        var result = containers.Select(container => container.ToResponse()).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContainerResponse>> GetByIdAsync([FromRoute] int id)
    {
        var container = await _containerRepository.GetByIdAsync(id);

        if (container == null) return NotFound();
        return Ok(container.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<int?>> UpdateAsync([FromRoute] int id,
        [FromBody] ContainerUpdateRequest updateRequest)
    {
        var result = await _containerRepository.UpdateAsync(id, updateRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int?>> CreateAsync([FromBody] ContainerCreateRequest createRequest)
    {
        var result = await _containerRepository.CreateAsync(createRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }
}