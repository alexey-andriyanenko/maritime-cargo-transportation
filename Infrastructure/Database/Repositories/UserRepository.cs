using Dapper;
using Domain.Company.Entities;
using Domain.User.Entities;
using Domain.User.Interfaces;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<User?> GetByEmailAsync(string email)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT users.id Id, users.first_name FirstName, users.last_name LastName, users.email Email FROM users WHERE email = @Email";

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql,
                new { Email = email });
        }
    }

    public async Task<List<User>> GetAllAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT users.id Id, users.first_name FirstName, users.last_name LastName, users.email Email, companies.id Id, companies.name Name FROM users JOIN users_to_companies ON users.id = users_to_companies.user_id JOIN companies ON users_to_companies.company_id = companies.id";

            var users = await _dbConnection.QueryAsync<User, Company, User>(sql,
                (user, company) =>
                {
                    if (user.Companies == null) user.Companies = new List<Company>();
                    user.Companies.Add(company);

                    return user;
                }, splitOn: "id");

            var result = users.GroupBy(user => user.Id).Select(group =>
                {
                    var groupedUser = group.First();

                    groupedUser.Companies = group.Select(user => user.Companies.Single()).ToList();

                    return groupedUser;
                })
                .ToList();

            return result;
        }
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT users.id Id, users.first_name FirstName, users.last_name LastName, users.email Email FROM users WHERE id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(
                sql,
                new { Id = id });
        }
    }

    public async Task<int?> CreateAsync(User user)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();
            var sql =
                "INSERT INTO users (email, password, first_name, last_name) VALUES (@Email, @Password, @FirstName, @LastName) RETURNING id";

            return await _dbConnection.ExecuteAsync(sql, user);
        }
    }
}