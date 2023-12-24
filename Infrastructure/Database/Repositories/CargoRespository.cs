using Dapper;
using Domain.Cargo.DTO;
using Domain.Cargo.Entities;
using Domain.Cargo.Interfaces;
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

            var sql =
                "SELECT cargoes.id Id, cargo_types.id Id, cargo_types.name Name from cargoes JOIN cargo_types ON cargoes.cargo_type_id = cargo_types.id";
            var result = await _dbConnection.QueryAsync<Cargo, CargoType, Cargo>(sql, (cargo, cargoType) =>
            {
                cargo.Type = cargoType;
                return cargo;
            }, splitOn: "Id");

            return result.ToList();
        }
    }

    public async Task<Cargo?> GetByIdAsync(int id)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "SELECT cargoes.id Id, cargo_types.id Id, cargo_types.name Name from cargoes JOIN cargo_types ON cargoes.cargo_type_id = cargo_types.id WHERE cargoes.id = @Id";
            var result = await _dbConnection.QueryAsync<Cargo, CargoType, Cargo>(sql, (cargo, cargoType) =>
            {
                cargo.Type = cargoType;
                return cargo;
            }, new { Id = id }, splitOn: "Id");

            return result.FirstOrDefault();
        }
    }

    public async Task<int?> UpdateAsync(int id, CargoUpdateRequest cargo)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "UPDATE cargoes SET cargo_type_id = @CargoTypeId WHERE cargoes.id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id, cargo.CargoTypeId });
        }
    }

    public async Task<int?> CreateAsync(CargoCreateRequest cargo)
    {
        using (var _dbConnection = new NpgsqlConnection(_connectionString))
        {
            _dbConnection.Open();

            var sql =
                "INSERT INTO cargoes (cargo_type_id) values (@CargoTypeId) RETURNING id";
            return await _dbConnection.ExecuteAsync(sql, cargo);
        }
    }
}