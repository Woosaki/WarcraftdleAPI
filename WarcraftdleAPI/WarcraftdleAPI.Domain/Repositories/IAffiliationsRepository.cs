using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Domain.Repositories;

public interface IAffiliationsRepository
{
    Task<IEnumerable<Affiliation>> GetAllAsync();
    Task<Affiliation?> GetByIdAsync(int id);
    Task<Affiliation?> GetByNameAsync(string name);
    Task<int> CreateAsync(Affiliation zone);
    Task DeleteAsync(Affiliation zone);
    bool Exists(string name);
}
