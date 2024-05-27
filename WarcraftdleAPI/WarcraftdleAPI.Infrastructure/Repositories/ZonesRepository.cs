using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Infrastructure.Repositories;

internal class ZonesRepository(WarcraftdleDbContext dbContext) : IZonesRepository
{
    public async Task<IEnumerable<Zone>> GetAsync(IEnumerable<string>? names = null)
    {
        var query = dbContext.Zone.AsQueryable();

        if (names != null)
        {
            query = query.Where(z => names.Contains(z.Name));
        }

        return await query.ToListAsync();
    }

    public async Task<Zone?> GetByIdAsync(int id)
    {
        var zone = await dbContext.Zone
            .FirstOrDefaultAsync(x => x.Id == id);

        return zone;
    }

    public async Task<Zone?> GetByNameAsync(string name)
    {
        var zone = await dbContext.Zone
            .FirstOrDefaultAsync(x => x.Name == name);

        return zone;
    }

    public async Task<int> CreateAsync(Zone zone)
    {
        dbContext.Zone.Add(zone);
        await dbContext.SaveChangesAsync();

        return zone.Id;
    }

    public async Task DeleteAsync(Zone zone)
    {
        dbContext.Zone.Remove(zone);
        await dbContext.SaveChangesAsync();
    }

    public bool Exists(string name)
    {
        return dbContext.Zone.Any(x => x.Name == name);
    }
}
