using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Interfaces;
using WarcraftdleAPI.Domain.Character;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Services;

public class AffiliationService(WowCharactersDbContext dbContext) : IAffiliationService
{
	public async Task<Affiliation?> GetByNameAsync(string name)
	{
		var affiliation = await dbContext.Affiliation
			.Where(a => a.Name == name)
			.FirstOrDefaultAsync();

		return affiliation;
	}

	public async Task AddAsync(string name)
	{
		var affiliation = new Affiliation { Name = name };

		await dbContext.Affiliation.AddAsync(affiliation);
		await dbContext.SaveChangesAsync();
	}
}
