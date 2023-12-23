using Domain.Company.DTO;
using Domain.Company.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Companies.DTO;

namespace Web.Controllers;

[Route("/companies")]
public class CompaniesController : Controller
{
    private readonly ICompanyRepository _companyRepository;

    public CompaniesController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CompanyResponse>>> GetListAsync()
    {
        var companies = await _companyRepository.GetListAsync();
        var result = companies.Select(company => company.ToResponse());

        return Ok(result);
    }

    [HttpGet("id/{id:int}")]
    public async Task<ActionResult<CompanyResponse>> GetByIdAsync([FromRoute] int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);

        if (company == null) return NotFound();
        return Ok(company.ToResponse());
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<CompanyResponse>> GetByNameAsync([FromRoute] string name)
    {
        var company = await _companyRepository.GetByNameAsync(name);

        if (company == null) return NotFound();
        return Ok(company.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<int>> UpdateAsync([FromRoute] int id, [FromBody] CompanyUpdateRequest companyRequest)
    {
        var result = await _companyRepository.UpdateAsync(id, companyRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAsync([FromBody] CompanyCreateRequest companyRequest)
    {
        var result = await _companyRepository.CreateAsync(companyRequest);

        if (result == null) return BadRequest();
        return Ok(result);
    }
}