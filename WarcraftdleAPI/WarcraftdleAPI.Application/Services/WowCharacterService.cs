using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Domain.Character;
using WarcraftdleAPI.Infrastructure;
using WarcraftdleAPI.Application.Interfaces;
using WarcraftdleAPI.Application.Dto;

namespace WarcraftdleAPI.Application.Services;

public class WowCharacterService(WowCharactersDbContext dbContext) : IWowCharacterService
{
	public async Task<IEnumerable<WowCharacter>> GetAsync()
	{
		var characters = await dbContext.WowCharacter.ToListAsync();

		return characters;
	}

	public async Task<WowCharacter> GetByIdAsync(int id)
	{
		var character = await dbContext.WowCharacter
			.FirstOrDefaultAsync(x => x.Id == id);

		return character;
	}

	public async Task<int> AddAsync(CharacterAddRequest request)
	{
		await dbContext.SaveChangesAsync();

		return 0;
	}
}
