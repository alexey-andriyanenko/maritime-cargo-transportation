using Dapper;
using Domain.User.Entities;
using Domain.User.Interfaces;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<User> GetByEmailAsync(string email)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email",
                new { Email = email });
        }
    }

    public async Task<List<User>> GetAllAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            UsersToCompaniesDB db;

            // query to get all users and their companies

        }
    }

    public async Task<User> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id",
                new { Id = id });
        }
    }

    public async Task<int?> CreateAsync(User user)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();
            var result = await _dbConnection.QueryFirstOrDefaultAsync<User>(
                "INSERT INTO Users (Id, Email, Password, FirstName, LastName, CreatedAt, UpdatedAt) VALUES (@Id, @Email, @Password, @FirstName, @LastName, @CreatedAt, @UpdatedAt) RETURNING *",
                new
                {
                    user.Id,
                    user.Email,
                    user.Password,
                    user.FirstName,
                    user.LastName
                });

            return result?.Id;
        }
    }
}