using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Cargo.DTO;

namespace Domain.Cargo.Interfaces;

public interface ICargoRepository
{
    public Task<List<Entities.Cargo>> GetListAsync();
    public Task<Entities.Cargo?> GetByIdAsync(int id);
    public Task<int?> UpdateAsync(int id, CargoUpdateRequest cargo);
    public Task<int?> CreateAsync(CargoCreateRequest cargo);
}