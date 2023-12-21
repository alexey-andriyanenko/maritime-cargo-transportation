using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Ship.Interfaces;

public interface IShipRepository
{
    public Task<Entities.Ship> GetShipByIdAsync(int id);
    public Task<List<Entities.Ship>> GetShipsListAsync();
    public Task<int> CreateShipAsync(Entities.Ship ship);
}