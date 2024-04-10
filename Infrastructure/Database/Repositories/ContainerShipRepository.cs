using System.Data;
using Dapper;
using Domain.Cargo.Entities;
using Domain.Container.Entities;
using Domain.Country.Entities;
using Domain.Ship.DTO;
using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class ContainerShipRepository : IContainerShipRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<ContainerShip>> GetListAsync(int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var result = await _dbConnection.QueryAsync<ContainerShipDb>(
                "SELECT * FROM get_container_ships_list(@CompanyId);",
                new { CompanyId = companyId }
            );

            return result
                .GroupBy(c => c.container_ship_id)
                .Select(g => g.First().ToDomain(g))
                .ToList();
        }
    }

    public async Task<ContainerShip?> GetByIdAsync(int id, int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var result = await _dbConnection.QueryAsync<ContainerShipDb>(
                "SELECT * FROM get_container_ship_by_id(@Id, @CompanyId);",
                new { Id = id, CompanyId = companyId }
            );

            return result
                .GroupBy(c => c.container_ship_id)
                .Select(g => g.First().ToDomain(g))
                .FirstOrDefault();
        }
    }

    public async Task<int?> CreateAsync(ContainerShipCreateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var id = await _dbConnection.ExecuteScalarAsync<int>(
                "SELECT create_ship(@Name, @CountryId, @ShipTypeId);",
                ship
            );
            return await _dbConnection.ExecuteScalarAsync<int>(
                "SELECT create_container_ship(@ShipId, @SizeTypeId);",
                new { ShipId = id, ship.SizeTypeId }
            );
        }
    }
}
