using Dapper;
using Domain.Company.DTO;
using Domain.Company.Entities;
using Domain.Company.Interfaces;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Company>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "SELECT companies.id Id, companies.name Name FROM companies";
            var result = await _dbConnection.QueryAsync<Company>(sql);

            return result.ToList();
        }
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "SELECT companies.id Id, companies.name Name FROM companies WHERE id=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Company>(sql, new { Id = id });
        }
    }

    public async Task<Company?> GetByNameAsync(string name)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "SELECT companies.id Id, companies.name Name FROM companies WHERE name=@Name";
            return await _dbConnection.QueryFirstOrDefaultAsync<Company>(sql, new { Name = name });
        }
    }

    public async Task<int?> UpdateAsync(int id, CompanyUpdateRequest company)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "UPDATE companies SET name = @Name WHERE id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id, company.Name });
        }
    }

    public async Task<int?> CreateAsync(CompanyCreateRequest company)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "INSERT INTO companies (name) values (@Name) RETURNING id";
            return await _dbConnection.ExecuteAsync(sql, company);
        }
    }
}