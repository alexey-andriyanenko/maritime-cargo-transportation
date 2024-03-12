using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Ship.DTO;
using Domain.Ship.Entities;

namespace Domain.Ship.Interfaces;

public interface IContainerShipRepository
{
    public Task<List<ContainerShip>> GetListAsync();
    public Task<ContainerShip> GetByIdAsync(int id);
    public Task<int?> CreateAsync(ContainerShipCreateRequest ship);
}