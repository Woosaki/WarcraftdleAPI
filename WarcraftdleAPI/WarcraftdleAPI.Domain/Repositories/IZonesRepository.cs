using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Domain.Repositories;

public interface IZonesRepository
{
    Task<IEnumerable<Zone>> GetAsync(IEnumerable<string>? names = null);
    Task<Zone?> GetByIdAsync(int id);
    Task<Zone?> GetByNameAsync(string name);
    Task<int> CreateAsync(Zone zone);
    Task DeleteAsync(Zone zone);
    bool Exists(string name);
}
