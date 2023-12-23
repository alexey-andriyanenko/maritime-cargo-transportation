using Dapper;
using Domain.Company.Entities;
using Domain.Flag.Entities;
using Domain.Ship.DTO;
using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Infrastructure.Database;
using Npgsql;

namespace Infrastructure.Repositories;

public class ShipRepository : IShipRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Ship>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT ships.id Id, ships.name Name, flags.id Id, flags.name Name, ship_types.id Id, ship_types.name Name, companies.id Id, companies.name Name FROM ships JOIN flags ON ships.flag_id = flags.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN companies ON ships.company_id = companies.id";

            var result = await _dbConnection.QueryAsync<Ship, Flag, ShipType, Company, Ship>(sql,
                (ship, flag, shipType, company) =>
                {
                    ship.Flag = flag;
                    ship.Type = shipType;
                    ship.Company = company;

                    return ship;
                }, splitOn: "id");

            return result.ToList();
        }
    }

    public async Task<Ship?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT ships.id Id, ships.name Name, flags.id Id, flags.name Name, ship_types.id Id, ship_types.name Name, companies.id Id, companies.name Name FROM ships JOIN flags ON ships.flag_id = flags.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN companies ON ships.company_id = companies.id WHERE ships.id = @Id";

            var result =
                await _dbConnection.QueryAsync<Ship, Flag, ShipType, Company, Ship>(sql,
                    (ship, flag, shipType, company) =>
                    {
                        ship.Flag = flag;
                        ship.Type = shipType;
                        ship.Company = company;

                        return ship;
                    }, new { Id = id },
                    splitOn: "id");

            return result.FirstOrDefault();
        }
    }

    public async Task<Ship?> GetByNameAsync(string name)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT ships.id Id, ships.name Name, flags.id Id, flags.name Name, ship_types.id Id, ship_types.name Name, companies.id Id, companies.name Name FROM ships JOIN flags ON ships.flag_id = flags.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN companies ON ships.company_id = companies.id WHERE ships.name = @Name";

            var result =
                await _dbConnection.QueryAsync<Ship, Flag, ShipType, Company, Ship>(sql,
                    (ship, flag, shipType, company) =>
                    {
                        ship.Flag = flag;
                        ship.Type = shipType;
                        ship.Company = company;

                        return ship;
                    }, new { Name = name },
                    splitOn: "id");

            return result.FirstOrDefault();
        }
    }

    public async Task<int?> UpdateAsync(int id, ShipUpdateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "UPDATE ships SET name = @Name, flag_id = @FlagId WHERE id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id, ship.Name, ship.FlagId });
        }
    }

    public async Task<int?> CreateAsync(ShipCreateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "INSERT INTO ships (name, flag_id, ship_type_id, company_id) VALUES (@Name, @FlagId, @ShipTypeId, @CompanyId) RETURNING id";
            return await _dbConnection.ExecuteAsync(sql, ship);
        }
    }
}