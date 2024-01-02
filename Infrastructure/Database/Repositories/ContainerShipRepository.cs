using Dapper;
using Domain.Flag.Entities;
using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Npgsql;

namespace Infrastructure.Database.Repositories;

public class ContainerShipRepository : IContainerShipRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<ContainerShip>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT ships.id Id, ships.name Name, flags.id Id, flags.name Name, ship_types.id Id, ship_types.name Name, container_ship_size_types.id Id, container_ship_size_types.name Name FROM container_ships JOIN ships ON container_ships.ship_id = ships.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN flags ON ships.flag_id = flags.id JOIN container_ship_size_types ON container_ships.size_id = container_ship_size_types.id";

            var result =
                await _dbConnection.QueryAsync<ContainerShip, Flag, ShipType, ContainerShipSizeType, ContainerShip>(sql,
                    (ship, flag, shipType, containerShipSizeType) =>
                    {
                        ship.Flag = flag;
                        ship.Type = shipType;
                        ship.SizeType = containerShipSizeType;

                        return ship;
                    }, splitOn: "Id");

            return result.ToList();
        }
    }
}