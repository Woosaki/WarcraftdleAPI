using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Infrastructure.Repositories;

internal class AffiliationsRepository(WarcraftdleDbContext dbContext) : IAffiliationsRepository
{
    public async Task<IEnumerable<Affiliation>> GetAsync(IEnumerable<string>? names = null)
    {
        var query = dbContext.Affiliation.AsQueryable();

        if (names != null)
        {
            query = query.Where(a => names.Contains(a.Name));
        }

        return await query.ToListAsync();
    }

    public async Task<Affiliation?> GetByIdAsync(int id)
    {
        var affiliation = await dbContext.Affiliation
            .FirstOrDefaultAsync(x => x.Id == id);

        return affiliation;
    }

    public async Task<Affiliation?> GetByNameAsync(string name)
    {
        var affiliation = await dbContext.Affiliation
            .FirstOrDefaultAsync(x => x.Name == name);

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
