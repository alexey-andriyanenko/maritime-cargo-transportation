using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Flag.Interfaces;

public interface IFlagRepository
{
    public Task<Entities.Flag> GetFlagByIdAsync(int id);
    public Task<List<Entities.Flag>> GetFlagsListAsync();
}