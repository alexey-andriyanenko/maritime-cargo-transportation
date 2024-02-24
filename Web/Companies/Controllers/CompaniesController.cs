using Domain.Company.DTO;
using Domain.Company.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Companies.DTO;

namespace Web.Controllers;

[Authorize]
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
        var userId = HttpContext.Session.GetInt32("user_id");
        if (userId == null) return BadRequest();

        Console.WriteLine($"COMPANY_ID {HttpContext.Session.GetInt32("company_id")}");

        var companies = await _companyRepository.GetListAsync(userId ?? 0);
        var result = companies.Select(company => company.ToResponse());

        return Ok(result);
    }

    [HttpGet("{companyId:int}")]
    public async Task<ActionResult<CompanyResponse>> GetByIdAsync([FromRoute] int companyId)
    {
        var userId = HttpContext.Session.GetInt32("user_id");
        if (userId == null) return BadRequest();

        var company = await _companyRepository.GetByIdAsync(userId ?? 0, companyId);

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