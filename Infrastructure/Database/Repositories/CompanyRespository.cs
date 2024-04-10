using System.Data;
using Dapper;
using Domain.Company.DTO;
using Domain.Company.Entities;
using Domain.Company.Interfaces;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Company>> GetListAsync(int userId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var result = await _dbConnection.QueryAsync<CompanyDb>(
                "SELECT * FROM get_companies_list(@UserId);",
                new { UserId = userId }
            );

            return result.Select(c => c.ToDomain()).ToList();
        }
    }

    public async Task<Company?> GetByIdAsync(int userId, int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var result = await _dbConnection.QueryFirstOrDefaultAsync<CompanyDb>(
                "SELECT * FROM get_company_by_id(@UserId, @CompanyId);",
                new { UserId = userId, CompanyId = companyId }
            );

            return result?.ToDomain();
        }
    }

    public async Task<int?> UpdateAsync(int id, CompanyUpdateRequest company)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            return await _dbConnection.ExecuteAsync(
                "SELECT update_company(@Id, @Name);",
                new { Id = id, company.Name }
            );
        }
    }

    public async Task<int?> CreateAsync(CompanyCreateRequest company)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            return await _dbConnection.ExecuteAsync("SELECT create_company(@Name);", company);
        }
    }
}
