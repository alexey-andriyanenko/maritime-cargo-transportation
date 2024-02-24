using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Ship.DTO;

namespace Domain.Ship.Interfaces;

public interface IShipRepository
{
    public Task<List<Entities.Ship>> GetListAsync(int companyId);
    public Task<Entities.Ship?> GetByIdAsync(int id);
    public Task<Entities.Ship?> GetByNameAsync(string name);
    public Task<int?> UpdateAsync(int id, ShipUpdateRequest ship);
    public Task<int?> CreateAsync(ShipCreateRequest ship);
}