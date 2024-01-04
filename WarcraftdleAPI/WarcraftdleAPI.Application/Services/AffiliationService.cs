using Microsoft.EntityFrameworkCore;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
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
		var affiliation = await dbContext.Affiliation.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ApiException($"Affiliation with id {id} could not be found", HttpStatusCode.NotFound);

		return affiliation;
	}
}
