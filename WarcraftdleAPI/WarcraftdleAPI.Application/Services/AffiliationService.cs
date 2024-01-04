using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.WowCharacter;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Services;

public class AffiliationService(WarcraftdleDbContext dbContext)
{
	public async Task<IEnumerable<Affiliation>> GetAsync()
	{
		var affiliations = await dbContext.Affiliation.ToListAsync();

		return affiliations;
	}

	public async Task<Affiliation> GetByIdAsync(int id)
	{
		var affiliation = await dbContext.Affiliation.FirstOrDefaultAsync(x => x.Id == id);

		return affiliation;
	}
}
