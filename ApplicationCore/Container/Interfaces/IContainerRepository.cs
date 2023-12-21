using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Container.Interfaces;

public interface IContainerRepository
{
    public Task<Entities.Container> GetContainerByIdAsync(int id);
    public Task<List<Entities.Container>> GetContainersListAsync();
    public Task<int> CreateContainerAsync(Entities.Container container);
}