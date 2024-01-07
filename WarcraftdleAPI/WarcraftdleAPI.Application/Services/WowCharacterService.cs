﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using WarcraftdleAPI.Application.Dto.WowCharacter;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.WowCharacter;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Services;

public class WowCharacterService(WarcraftdleDbContext dbContext)
{
	public async Task<IEnumerable<WowCharacter>> GetAsync()
	{
		var wowCharacters = await dbContext.WowCharacter.ToListAsync();

		return wowCharacters;
	}

	public async Task<WowCharacter> GetByIdAsync(int id)
	{
		var wowCharacter = await dbContext.WowCharacter.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ApiException($"WowCharacter with id {id} could not be found", HttpStatusCode.NotFound);

		return wowCharacter;
	}

	public async Task<int> AddAsync(AddWowCharacterRequest request)
	{
		var gender = await dbContext.Gender.FirstOrDefaultAsync(x => x.Name == request.Name)
			?? throw new ApiException($"Gender {request.Name} could not be found", HttpStatusCode.NotFound);
		var race = await dbContext.Race.FirstOrDefaultAsync(x => x.Name == request.Name)
			?? throw new ApiException($"Race {request.Name} could not be found", HttpStatusCode.NotFound);
		var @class = await dbContext.Class.FirstOrDefaultAsync(x => x.Name == request.Name);
		var expansions = await dbContext.Expansion.Where(x => request.Expansions.Contains(x.Name)).ToListAsync();
		var affiliations = await dbContext.Affiliation.Where(x => request.Affiliations.Contains(x.Name)).ToListAsync();
		var zones = await dbContext.Zone.Where(x => request.Zones.Contains(x.Name)).ToListAsync();

		var wowCharacter = new WowCharacter 
		{ 
			Name = request.Name,
			Photo = request.Photo,
			Gender = gender,
			Race = race,
			Class = @class,
			Expansions = expansions,
			Affiliations = affiliations,
			Zones = zones
		};

		await dbContext.WowCharacter.AddAsync(wowCharacter);
		await dbContext.SaveChangesAsync();

		return wowCharacter.Id;
	}

	public async Task DeleteAsync(int id)
	{
		var wowCharacter = await dbContext.WowCharacter.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ApiException($"WowCharacter with id {id} could not be found", HttpStatusCode.NotFound);

		dbContext.WowCharacter.Remove(wowCharacter);
		await dbContext.SaveChangesAsync();
	}
}
