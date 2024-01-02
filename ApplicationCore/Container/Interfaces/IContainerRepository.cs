using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Container.DTO;

namespace Domain.Container.Interfaces;

public interface IContainerRepository
{
    public Task<List<Entities.Container>> GetListAsync();
    public Task<Entities.Container?> GetByIdAsync(int id);
    public Task<int?> UpdateAsync(int id, ContainerUpdateRequest container);
    public Task<int?> CreateAsync(ContainerCreateRequest container);
}