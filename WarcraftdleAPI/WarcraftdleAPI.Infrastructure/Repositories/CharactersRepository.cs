using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Infrastructure.Repositories;

internal class CharactersRepository(WarcraftdleDbContext dbContext) : ICharactersRepository
{
    public async Task<IEnumerable<Character>> GetAsync(string? startsWith = null)
    {
        var query = dbContext.Character.AsQueryable();

        if(!string.IsNullOrEmpty(startsWith))
        {
            query = query.Where(x => x.Name.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase));
        }

        return await query.ToListAsync();
    }

    public async Task<Character?> GetByIdAsync(int id)
    {
        var character = await dbContext.Character
            .FirstOrDefaultAsync(x => x.Id == id);

        return character;
    }

    public async Task<int> CreateAsync(Character character)
    {
        dbContext.Character.Add(character);
        await dbContext.SaveChangesAsync();

        return character.Id;
    }

    public async Task DeleteAsync(Character character)
    {
        dbContext.Character.Remove(character);
        await dbContext.SaveChangesAsync();
    }

    public bool ExistsWithName(string name)
    {
        return dbContext.Character.Any(x => x.Name == name);
    }
}
