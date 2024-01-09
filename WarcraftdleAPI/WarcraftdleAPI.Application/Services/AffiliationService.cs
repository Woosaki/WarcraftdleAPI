using Microsoft.EntityFrameworkCore;
using System.Net;
using WarcraftdleAPI.Application.Dtos.Affiliations;
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

	public async Task<int> AddAsync(AddAffiliationRequest request)
	{
		var affiliation = new Affiliation { Name = request.Name };

		await dbContext.Affiliation.AddAsync(affiliation);
		await dbContext.SaveChangesAsync();

		return affiliation.Id;
	}

	public async Task AddMultipleAsync(AddMultipleAffiliationRequest request)
	{
		var affiliations = new List<Affiliation>();

		foreach (var name in request.AffiliationNames)
		{
			affiliations.Add(new Affiliation { Name = name });
		}

		await dbContext.Affiliation.AddRangeAsync(affiliations);
		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var affiliation = await dbContext.Affiliation.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ApiException($"Affiliation with id {id} could not be found", HttpStatusCode.NotFound);

		dbContext.Affiliation.Remove(affiliation);
		await dbContext.SaveChangesAsync();
	}
}
