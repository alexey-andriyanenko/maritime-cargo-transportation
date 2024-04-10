using System.Data;
using Dapper;
using Domain.Cargo.DTO;
using Domain.Cargo.Entities;
using Domain.Cargo.Interfaces;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Cargo>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<CargoDb>(
                "SELECT * FROM get_cargoes_list();"
            );

            return result.Select(c => c.ToDomain()).ToList();
        }
    }

    public async Task<Cargo?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var result = await _dbConnection.QueryAsync<CargoDb>(
                "SELECT * FROM get_cargo_by_id(@Id);",
                new { Id = id }
            );

            return result.First()?.ToDomain();
        }
    }

    public async Task<int?> UpdateAsync(int id, CargoUpdateRequest cargo)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();
            return await _dbConnection.ExecuteAsync(
                "SELECT update_cargo(@Id, @CargoTypeId);",
                new { Id = id, cargo.CargoTypeId }
            );
        }
    }

    public async Task<int?> CreateAsync(CargoCreateRequest cargo)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            return await _dbConnection.ExecuteAsync("SELECT create_cargo(@CargoTypeId);", cargo);
        }
    }
}
