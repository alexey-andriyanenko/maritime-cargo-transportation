using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Ship.Entities;

namespace Domain.Ship.Interfaces;

public interface IContainerShipRepository
{
    public Task<ContainerShip> GetContainerShipByIdAsync(int id);
    public Task<List<ContainerShip>> GetContainerShipsListAsync();
    public Task<int> CreateContainerShipAsync(ContainerShip containerShip);
}