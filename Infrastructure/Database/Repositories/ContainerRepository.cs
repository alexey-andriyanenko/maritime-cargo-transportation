using Dapper;
using Domain.Container.DTO;
using Domain.Container.Entities;
using Domain.Container.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Npgsql;

namespace Infrastructure.Repositories;

public class ContainerRepository : IContainerRepository
{
    private readonly string _connectionString = DatabaseHelpers.GetConnectionString();

    public async Task<List<Container>> GetListAsync()
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT containers.id ContainerId, containers.container_ship_id ContainerShipId, container_types.id ContainerTypeId, container_types.name ContainerTypeName, cargoes.id CargoId, cargo_types.id CargoTypeId, cargo_types.name CargoTypeName FROM containers JOIN container_types ON containers.container_type_id = container_types.id JOIN cargoes_to_containers ON containers.id = cargoes_to_containers.container_id JOIN cargoes ON  cargoes_to_containers.cargo_id = cargoes.id JOIN cargo_types ON cargoes.cargo_type_id = cargo_types.id";

            var result =
                await _dbConnection
                    .QueryAsync<ContainerDB>(sql);
            var containers = result.ToList()
                .Select(dbModel => dbModel.ToDomain());

            return containers.ToList();
        }
    }

    public async Task<Container?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "SELECT containers.id ContainerId, containers.container_ship_id ContainerShipId, container_types.id ContainerTypeId, container_types.name ContainerTypeName, cargoes.id CargoId, cargo_types.id CargoTypeId, cargo_types.name CargoTypeName FROM containers JOIN container_types ON containers.container_type_id = container_types.id JOIN cargoes_to_containers ON containers.id = cargoes_to_containers.container_id JOIN cargoes ON  cargoes_to_containers.cargo_id = cargoes.id JOIN cargo_types ON cargoes.cargo_type_id = cargo_types.id WHERE containers.id = @Id";
            var result = await _dbConnection.QueryAsync<ContainerDB>(sql, new { Id = id });
            var containers = result.ToList().Select(dbModel => dbModel.ToDomain())
                .FirstOrDefault();

            return containers;
        }
    }

    public async Task<int?> UpdateAsync(int id, ContainerUpdateRequest container)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql = "UPDATE containers SET container_ship_id = @ContainerShipId WHERE id = @Id";
            var result =
                await _dbConnection.ExecuteAsync(sql, new { Id = id, container.ContainerShipId, container.CargoId });

            return result;
        }
    }

    public async Task<int?> CreateAsync(ContainerCreateRequest container)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var sql =
                "INSERT INTO containers (container_ship_id, container_type_id) values (@ContainerShipId, @ContainerTypeId) RETURNING id";
            var result = await _dbConnection.ExecuteAsync(sql, container);

            return result;
        }
    }
}