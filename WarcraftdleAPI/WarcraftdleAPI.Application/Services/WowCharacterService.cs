using Microsoft.EntityFrameworkCore;
using System.Net;
using WarcraftdleAPI.Application.Dtos.WowCharacters;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Services;

public class WowCharacterService(WarcraftdleDbContext dbContext)
{
    public async Task<IEnumerable<WowCharacterDto>> GetAsync(string? startsWith)
    {
        var wowCharacters = dbContext.Character.AsQueryable();

        var wowCharactersDtos = await wowCharacters
            .Select(x => new WowCharacterDto
            (
                x.Id,
                x.Name,
                x.Photo,
                x.Gender.Name,
                x.Race.Name,
                x.Class == null ? null : x.Class.Name,
                x.Expansions.Select(e => e.Name).ToList(),
                x.Affiliations.Select(a => a.Name).ToList(),
                x.Zones.Select(z => z.Name).ToList()
            )).ToListAsync();

        if (startsWith == null)
        {
            return wowCharactersDtos;
        }

        return wowCharactersDtos
           .Where(w => w.Name.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<WowCharacterDto> GetByIdAsync(int id)
    {
        var wowCharacterDto = await dbContext.Character
            .Where(x => x.Id == id)
            .Select(x => new WowCharacterDto
            (
                x.Id,
                x.Name,
                x.Photo,
                x.Gender.Name,
                x.Race.Name,
                x.Class == null ? null : x.Class.Name,
                x.Expansions.Select(e => e.Name).ToList(),
                x.Affiliations.Select(a => a.Name).ToList(),
                x.Zones.Select(z => z.Name).ToList()
            ))
            .FirstOrDefaultAsync()
            ?? throw new ApiException($"WowCharacter with id {id} could not be found", HttpStatusCode.NotFound);

        return wowCharacterDto;
    }

    public async Task<WowCharacterDto> GetRandomAsync()
    {
        var characterCount = await dbContext.Character.CountAsync();

        var randomIndex = new Random().Next(0, characterCount);

        var wowCharacterDto = await dbContext.Character
            .Skip(randomIndex).Take(1)
            .Select(x => new WowCharacterDto
            (
                x.Id,
                x.Name,
                x.Photo,
                x.Gender.Name,
                x.Race.Name,
                x.Class == null ? null : x.Class.Name,
                x.Expansions.Select(e => e.Name).ToList(),
                x.Affiliations.Select(a => a.Name).ToList(),
                x.Zones.Select(z => z.Name).ToList()
            ))
            .FirstOrDefaultAsync()
            ?? throw new ApiException($"There are no characters", HttpStatusCode.NotFound);

        return wowCharacterDto;
    }

    public async Task<int> AddAsync(AddWowCharacterRequest request)
    {
        var gender = await dbContext.Gender.FirstOrDefaultAsync(x => x.Name == request.Gender);
        var race = await dbContext.Race.FirstOrDefaultAsync(x => x.Name == request.Race);
        var @class = await dbContext.Class.FirstOrDefaultAsync(x => x.Name == request.Class);
        var expansions = await dbContext.Expansion.Where(x => request.Expansions.Contains(x.Name)).ToListAsync();
        var affiliations = await dbContext.Affiliation.Where(x => request.Affiliations.Contains(x.Name)).ToListAsync();
        var zones = await dbContext.Zone.Where(x => request.Zones.Contains(x.Name)).ToListAsync();

        var wowCharacter = new Character
        {
            Name = request.Name,
            Photo = request.Photo,
            Gender = gender!,
            Race = race!,
            Class = @class,
            Expansions = expansions,
            Affiliations = affiliations,
            Zones = zones
        };

        await dbContext.Character.AddAsync(wowCharacter);
        await dbContext.SaveChangesAsync();

        return wowCharacter.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var wowCharacter = await dbContext.Character.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ApiException($"WowCharacter with id {id} could not be found", HttpStatusCode.NotFound);

        dbContext.Character.Remove(wowCharacter);
        await dbContext.SaveChangesAsync();
    }
}