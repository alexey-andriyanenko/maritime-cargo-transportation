using Dapper;
using Domain.Company.DTO;
using Domain.Company.Entities;
using Domain.Company.Interfaces;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Company>> GetListAsync(int userId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT companies.id Id, companies.name Name FROM companies JOIN users_to_companies ON users_to_companies.user_id = @UserId AND users_to_companies.company_id = companies.id";
            var result = await _dbConnection.QueryAsync<Company>(sql, new { UserId = userId });

            return result.ToList();
        }
    }

    public async Task<Company?> GetByIdAsync(int userId, int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))

        {
            var sql =
                "SELECT companies.id Id, companies.name Name FROM companies JOIN users_to_companies ON users_to_companies.user_id = @UserId AND users_to_companies.company_id = @CompanyId AND companies.id = @CompanyId";
            return await _dbConnection.QueryFirstOrDefaultAsync<Company>(sql,
                new { UserId = userId, CompanyId = companyId });
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