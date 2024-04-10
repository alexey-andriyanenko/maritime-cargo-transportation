using System.Data;
using Dapper;
using Domain.Company.Entities;
using Domain.Country.Entities;
using Domain.Ship.DTO;
using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Repositories;

public class ShipRepository : IShipRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Ship>> GetListAsync(int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var result = await _dbConnection.QueryAsync<ShipDb>(
                "SELECT * FROM get_ships_list(@CompanyId);",
                param: new { CompanyId = companyId }
            );

            return result.Select(ship => ship.ToDomain()).ToList();
        }
    }

    public async Task<Ship?> GetByIdAsync(int id, int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var result = await _dbConnection.QueryAsync<ShipDb>(
                "SELECT * FROM get_ship_by_id(@Id, @CompanyId);",
                new { Id = id, CompanyId = companyId }
            );

            return result.Select(ship => ship.ToDomain()).FirstOrDefault();
        }
    }

    public async Task<int?> UpdateAsync(int id, ShipUpdateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            return await _dbConnection.ExecuteAsync(
                "update_ship",
                new
                {
                    Id = id,
                    ship.Name,
                    ship
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }

    public async Task<int?> CreateAsync(ShipCreateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            return await _dbConnection.ExecuteAsync(
                "create_ship",
                ship,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
