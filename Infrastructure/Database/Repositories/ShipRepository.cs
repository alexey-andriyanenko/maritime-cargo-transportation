using Dapper;
using Domain.Company.Entities;
using Domain.Country.Entities;
using Domain.Ship.DTO;
using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Infrastructure.Database;
using Npgsql;

namespace Infrastructure.Repositories;

public class ShipRepository : IShipRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Ship>> GetListAsync(int companyId)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT ships.id Id, ships.name Name, countries.id Id, countries.name Name, countries.country_code CountryCode, countries.code Code, ship_types.id Id, ship_types.name Name, companies.id Id, companies.name Name FROM ships JOIN countries ON ships.country_id = countries.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN companies ON ships.company_id = companies.id WHERE companies.id = @CompanyId";

            var result = await _dbConnection.QueryAsync<Ship, Country, ShipType, Company, Ship>(sql,
                (ship, flag, shipType, company) =>
                {
                    ship.Country = flag;
                    ship.Type = shipType;
                    ship.Company = company;

                    return ship;
                }, splitOn: "id", param: new { CompanyId = companyId });

            return result.ToList();
        }
    }

    public async Task<Ship?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT ships.id Id, ships.name Name, countries.id Id, countries.name Name, countries.country_code CountryCode, countries.code Code, ship_types.id Id, ship_types.name Name, companies.id Id, companies.name Name FROM ships JOIN countries ON ships.country_id = countries.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN companies ON ships.company_id = companies.id WHERE ships.id = @Id";

            var result =
                await _dbConnection.QueryAsync<Ship, Country, ShipType, Company, Ship>(sql,
                    (ship, country, shipType, company) =>
                    {
                        ship.Country = country;
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
                "SELECT ships.id Id, ships.name Name, countries.id Id, countries.name Name, ship_types.id Id, ship_types.name Name, companies.id Id, companies.name Name FROM ships JOIN countries ON ships.country_id = countries.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN companies ON ships.company_id = companies.id WHERE ships.name = @Name";

            var result =
                await _dbConnection.QueryAsync<Ship, Country, ShipType, Company, Ship>(sql,
                    (ship, country, shipType, company) =>
                    {
                        ship.Country = country;
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
            var sql = "UPDATE ships SET name = @Name, country_id = @CountryId WHERE id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id, ship.Name, ship });
        }
    }

    public async Task<int?> CreateAsync(ShipCreateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "INSERT INTO ships (name, country_id, ship_type_id, company_id) VALUES (@Name, @CountryId, @ShipTypeId, @CompanyId) RETURNING id";
            return await _dbConnection.ExecuteAsync(sql, ship);
        }
    }
}