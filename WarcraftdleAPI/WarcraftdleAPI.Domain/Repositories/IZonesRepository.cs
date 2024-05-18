using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Domain.Repositories;

public interface IZonesRepository
{
    Task<IEnumerable<Zone>> GetAllAsync();
    Task<Zone?> GetByIdAsync(int id);
    Task<int> CreateAsync(Zone zone);
    Task DeleteAsync(Zone zone);
    bool Exists(string name);
}
