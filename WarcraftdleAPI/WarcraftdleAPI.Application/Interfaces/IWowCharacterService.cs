using WarcraftdleAPI.Application.Dto;
using WarcraftdleAPI.Domain.Character;

namespace WarcraftdleAPI.Application.Interfaces;

public interface IWowCharacterService
{
	Task<int> AddAsync(CharacterAddRequest request);
	Task<IEnumerable<WowCharacter>> GetAsync();
	Task<WowCharacter> GetByIdAsync(int id);
}
