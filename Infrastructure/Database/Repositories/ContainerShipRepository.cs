using Dapper;
using Domain.Cargo.Entities;
using Domain.Container.Entities;
using Domain.Flag.Entities;
using Domain.Ship.Entities;
using Domain.Ship.Interfaces;
using Infrastructure.Database.Entities;
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
                "SELECT container_ships.id Id, ships.id ShipId, ships.name ShipName, flags.id FlagId, flags.name FlagName, ship_types.id ShipTypeId, ship_types.name ShipTypeName, container_ship_size_types.id SizeTypeId, container_ship_size_types.name SizeTypeName, containers.id ContainerId, container_types.id ContainerTypeId, container_types.name ContainerTypeName, cargoes.id CargoId, cargo_types.id CargoTypeId, cargo_types.name CargoTypeName  FROM container_ships JOIN ships ON container_ships.ship_id = ships.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN flags ON ships.flag_id = flags.id JOIN container_ship_size_types ON container_ships.size_id = container_ship_size_types.id join containers on containers.container_ship_id = container_ships.id join container_types on container_types.id = containers.container_type_id join cargoes_to_containers on cargoes_to_containers.container_id = containers.id join cargoes on cargoes_to_containers.cargo_id = cargoes.id join cargo_types on cargo_types.id = cargoes.cargo_type_id";
            var data = await _dbConnection.QueryAsync<ContainerShipDB>(sql);
            var result = data.ToList().GroupBy(x => x.Id).Select(group =>
            {
                var first = group.First();
                var ship = new ContainerShip
                {
                    Id = first.ShipId,
                    Name = first.ShipName,
                    Flag = new Flag { Id = first.FlagId, Name = first.FlagName },
                    Type = new ShipType { Id = first.ShipTypeId, Name = first.ShipTypeName },
                    SizeType = new ContainerShipSizeType { Id = first.SizeTypeId, Name = first.SizeTypeName },
                    Containers = new List<Container>()
                };

                foreach (var containerShipDb in group)
                {
                    var container = new Container
                    {
                        Id = containerShipDb.ContainerId,
                        ContainerShipId = containerShipDb.Id,
                        Type = new ContainerType
                            { Id = containerShipDb.ContainerTypeId, Name = containerShipDb.ContainerTypeName },
                        Cargo = new Cargo
                        {
                            Id = containerShipDb.CargoId,
                            Type = new CargoType
                                { Id = containerShipDb.CargoTypeId, Name = containerShipDb.CargoTypeName }
                        }
                    };

                    ship.Containers.Add(container);
                }

                return ship;
            });

            return result.ToList();
        }
    }
}