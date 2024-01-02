using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Ship.Entities;

namespace Domain.Ship.Interfaces;

public interface IContainerShipRepository
{
    public Task<List<ContainerShip>> GetListAsync();
}