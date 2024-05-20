using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Domain.Repositories;

public interface ICharactersRepository
{
    Task<IEnumerable<Character>> GetAsync(string? startsWith = null);
    Task<Character?> GetByIdAsync(int id);
    Task<int> CreateAsync(Character character);
    Task DeleteAsync(Character character);
}
