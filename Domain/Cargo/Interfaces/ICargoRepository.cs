using System.Threading.Tasks;

namespace Domain.Cargo.Interfaces;

public interface ICargoRepository
{
    public Task<Entities.Cargo> GetCargoTypeByIdAsync(int id);
    public Task<Entities.Cargo> GetCargoTypeByNameAsync(string name);
}