using Microsoft.EntityFrameworkCore;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.WowCharacter;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Services;

public class ZoneService(WarcraftdleDbContext dbContext)
{
	public async Task<IEnumerable<Zone>> GetAsync()
	{
		var zones = await dbContext.Zone.ToListAsync();

		return zones;
	}

	public async Task<Zone> GetByIdAsync(int id)
	{
		var zone = await dbContext.Zone.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ApiException($"Zone with id {id} could not be found", HttpStatusCode.NotFound);

		return zone;
	}

	public async Task<int> AddAsync(string name)
	{
		var zone = new Zone { Name = name };

		await dbContext.Zone.AddAsync(zone);
		await dbContext.SaveChangesAsync();

		return zone.Id;
	}

	public async Task AddMultipleAsync(IEnumerable<string> zoneNames)
	{
		var zones = new List<Zone>();

		foreach (var name in zoneNames)
		{
			zones.Add(new Zone { Name = name });
		}

		await dbContext.Zone.AddRangeAsync(zones);
		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var zone = await dbContext.Zone.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ApiException($"Zone with id {id} could not be found", HttpStatusCode.NotFound);

		dbContext.Zone.Remove(zone);
		await dbContext.SaveChangesAsync();
	}
}
