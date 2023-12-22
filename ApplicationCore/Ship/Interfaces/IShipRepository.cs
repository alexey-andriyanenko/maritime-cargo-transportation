using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Ship.Interfaces;

public interface IShipRepository
{
    public Task<Entities.Ship?> GetByIdAsync(int id);
    public Task<List<Entities.Ship>> GetListAsync();
}