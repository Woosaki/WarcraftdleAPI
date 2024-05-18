using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Infrastructure.Repositories;

internal class AffiliationsRepository(WarcraftdleDbContext dbContext) : IAffiliationsRepository
{
    public async Task<IEnumerable<Affiliation>> GetAllAsync()
    {
        var affiliations = await dbContext.Affiliation.ToListAsync();

        return affiliations;
    }

    public async Task<Affiliation?> GetByIdAsync(int id)
    {
        var affiliation = await dbContext.Affiliation
            .FirstOrDefaultAsync(x => x.Id == id);

        return affiliation;
    }

    public async Task<int> CreateAsync(Affiliation affiliation)
    {
        dbContext.Affiliation.Add(affiliation);
        await dbContext.SaveChangesAsync();

        return affiliation.Id;
    }

    public async Task DeleteAsync(Affiliation affiliation)
    {
        dbContext.Affiliation.Remove(affiliation);
        await dbContext.SaveChangesAsync();
    }

    public bool Exists(string name)
    {
        return dbContext.Affiliation.Any(x => x.Name == name);
    }
}
