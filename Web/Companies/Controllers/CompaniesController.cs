using Domain.Company.Entities;
using Domain.Company.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<Company>>> GetAllCompaniesAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("id/{id:int}")]
    public async Task<ActionResult<Company?>> GetCompanyByIdAsync([FromRoute] int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);

        if (company == null) return NotFound();
        return Ok(company);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<Company?>> GetCompanyByNameAsync([FromRoute] string name)
    {
        var company = await _companyRepository.GetByNameAsync(name);

        if (company == null) return NotFound();
        return Ok(company);
    }
}