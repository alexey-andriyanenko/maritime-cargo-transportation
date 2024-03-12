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

    public async Task<List<ContainerShip>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT container_ships.id ContainerShipId, ships.id ShipId, ships.name ShipName, countries.id CountryId, countries.name CountryName, countries.country_code CountryCountryCode, countries.code CountryCode, ship_types.id ShipTypeId, ship_types.name ShipTypeName, container_ship_size_types.id SizeTypeId, container_ship_size_types.name SizeTypeName, containers.id ContainerId, container_types.id ContainerTypeId, container_types.name ContainerTypeName, cargoes.id CargoId, cargo_types.id CargoTypeId, cargo_types.name CargoTypeName FROM container_ships JOIN ships ON container_ships.ship_id = ships.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN countries ON ships.country_id = countries.id JOIN container_ship_size_types ON container_ships.size_id = container_ship_size_types.id LEFT JOIN containers ON containers.container_ship_id = container_ships.id LEFT JOIN container_types ON container_types.id = containers.container_type_id LEFT JOIN cargoes_to_containers ON cargoes_to_containers.container_id = containers.id LEFT JOIN cargoes ON cargoes_to_containers.cargo_id = cargoes.id LEFT JOIN cargo_types ON cargo_types.id = cargoes.cargo_type_id";
            var data = await _dbConnection.QueryAsync<ContainerShipDB>(sql);
            var result = data.ToList().GroupBy(x => x.ContainerShipid).Select(group =>
            {
                var first = group.First();
                var ship = new ContainerShip
                {
                    Id = first.ShipId,
                    ContainerShipId = first.ContainerShipid,
                    Name = first.ShipName,
                    Country = new Country { Id = first.CountryId, Name = first.CountryName, CountryCode = first.CountryCountryCode, Code = first.CountryCode},
                    Type = new ShipType { Id = first.ShipTypeId, Name = first.ShipTypeName },
                    SizeType = new ContainerShipSizeType { Id = first.SizeTypeId, Name = first.SizeTypeName },
                    Containers = new List<Container>()
                };

                foreach (var containerShipDb in group)
                {
                    if (containerShipDb.ContainerId == 0) break;

                    var container = new Container
                    {
                        Id = containerShipDb.ContainerId,
                        ContainerShipId = containerShipDb.ContainerShipid,
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
    
        public async Task<ContainerShip> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT container_ships.id ContainerShipId, ships.id ShipId, ships.name ShipName, countries.id CountryId, countries.name FlagName, countries.country_code CountryCountryCode, countries.code CountryCode, ship_types.id ShipTypeId, ship_types.name ShipTypeName, container_ship_size_types.id SizeTypeId, container_ship_size_types.name SizeTypeName, containers.id ContainerId, container_types.id ContainerTypeId, container_types.name ContainerTypeName, cargoes.id CargoId, cargo_types.id CargoTypeId, cargo_types.name CargoTypeName FROM container_ships JOIN ships ON container_ships.ship_id = ships.id JOIN ship_types ON ships.ship_type_id = ship_types.id JOIN countries ON ships.country_id = countries.id JOIN container_ship_size_types ON container_ships.size_id = container_ship_size_types.id LEFT JOIN containers ON containers.container_ship_id = container_ships.id LEFT JOIN container_types ON container_types.id = containers.container_type_id LEFT JOIN cargoes_to_containers ON cargoes_to_containers.container_id = containers.id LEFT JOIN cargoes ON cargoes_to_containers.cargo_id = cargoes.id LEFT JOIN cargo_types ON cargo_types.id = cargoes.cargo_type_id WHERE ships.id = @Id";
            
            var data = await _dbConnection.QueryAsync<ContainerShipDB>(sql, param: new { Id = id });
            var result = data.ToList().GroupBy(x => x.ContainerShipid).Select(group =>
            {
                var first = group.First();
                var ship = new ContainerShip
                {
                    Id = first.ShipId,
                    ContainerShipId = first.ContainerShipid,
                    Name = first.ShipName,
                    Country = new Country { Id = first.CountryId, Name = first.CountryName, CountryCode = first.CountryCountryCode, Code = first.CountryCode },
                    Type = new ShipType { Id = first.ShipTypeId, Name = first.ShipTypeName },
                    SizeType = new ContainerShipSizeType { Id = first.SizeTypeId, Name = first.SizeTypeName },
                    Containers = new List<Container>()
                };

                foreach (var containerShipDb in group)
                {
                    if (containerShipDb.ContainerId == 0) break;

                    var container = new Container
                    {
                        Id = containerShipDb.ContainerId,
                        ContainerShipId = containerShipDb.ContainerShipid,
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

            return result.FirstOrDefault();
        }
    }

    public async Task<int?> CreateAsync(ContainerShipCreateRequest ship)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var createShipSql =
                "INSERT INTO ships (name, country_id, ship_type_id, company_id) VALUES (@Name, @CountryId, @ShipTypeId, @CompanyId) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(createShipSql, ship);

            var createContainerShipSql =
                "INSERT INTO container_ships (ship_id, size_id) VALUES (@ShipId, @SizeTypeId) RETURNING id";
            return await _dbConnection.ExecuteScalarAsync<int>(createContainerShipSql,
                new { ShipId = id, ship.SizeTypeId });
        }
    }
}