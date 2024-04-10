using System.Data;
using Dapper;
using Domain.Company.Entities;
using Domain.User.DTO;
using Domain.User.Entities;
using Domain.User.Interfaces;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<User>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var users = await _dbConnection.QueryAsync<UserDb>("SELECT * FROM get_users_list();");

            return users
                .GroupBy(u => u.user_id)
                .Select(group => group.First().ToDomain(group))
                .ToList();
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var users = await _dbConnection.QueryAsync<UserDb>(
                "SELECT * FROM get_user_by_email(@Email);",
                new { Email = email }
            );

            if (users.Count() == 0)
                return null;

            return users
                .GroupBy(u => u.user_id)
                .Select(group => group.First().ToDomain(group))
                .FirstOrDefault();
        }
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var users = await _dbConnection.QueryAsync<UserDb>(
                "SELECT * FROM get_user_by_id(@Id);",
                new { Id = id }
            );

            if (users.Count() == 0)
                return null;

            return users
                .GroupBy(u => u.user_id)
                .Select(group => group.First().ToDomain(group))
                .First();
        }
    }

    public async Task<int?> UpdateAsync(int id, UserUpdateRequest user)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            return await _dbConnection.ExecuteAsync(
                "SELECT update_user(@Id, @FirstName, @LastName, @Email);",
                new
                {
                    Id = id,
                    user.FirstName,
                    user.LastName,
                    user.Email
                }
            );
        }
    }

    public async Task<int?> CreateAsync(UserCreateRequest user)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            return await _dbConnection.ExecuteAsync(
                "SELECT create_user(@FirstName, @LastName, @Email, @Password);",
                user,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
