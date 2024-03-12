using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Country.Interfaces;

public interface ICountryRepository
{
    public Task<Entities.Country> GetCountryByIdAsync(int id);
    public Task<List<Entities.Country>> GetCountriesListAsync();
}
