using System.Data;
using System.Text;
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

            var result = await _dbConnection.QueryAsync<ContainerDb>(
                "SELECT * FROM get_containers_list();"
            );
            var containers = result.ToList().Select(dbModel => dbModel.ToDomain());

            return containers.ToList();
        }
    }

    public async Task<Container?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            var result = await _dbConnection.QueryAsync<ContainerDb>(
                "SELECT * FROM get_container_by_id(@Id)",
                new { Id = id }
            );

            return result.First()?.ToDomain();
        }
    }

    public async Task<int?> UpdateAsync(int id, ContainerUpdateRequest container)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            return await _dbConnection.ExecuteAsync(
                "SELECT update_container(@Id, @ContainerShipId, @CargoId);",
                new
                {
                    Id = id,
                    container.ContainerShipId,
                    container.CargoId
                }
            );
        }
    }

    public async Task<int?> CreateAsync(ContainerCreateRequest container)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            return await _dbConnection.ExecuteAsync(
                "SELECT create_container(@ContainerShipId, @CargoId);",
                container
            );
        }
    }

    public async Task<int?> AttachToContainerShip(int containerShipId, int[] containersIds)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var stringBuilder = new StringBuilder();

            foreach (var containerId in containersIds)
            {
                stringBuilder.Append(
                    $"SELECT attach_container_to_container_ship({containerId}, {containerShipId});"
                );
            }

            return await _dbConnection.ExecuteAsync(stringBuilder.ToString());
        }
    }

    public async Task<int?> DetachFromContainerShip(int[] containersIds)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var stringBuilder = new StringBuilder();

            foreach (var containerId in containersIds)
            {
                stringBuilder.Append(
                    $"SELECT detach_container_from_container_ship({containerId});"
                );
            }

            return await _dbConnection.ExecuteAsync(stringBuilder.ToString());
        }
    }
}
