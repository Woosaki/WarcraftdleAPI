using WarcraftdleAPI.Domain.Character;

namespace WarcraftdleAPI.Domain.Interfaces;

public interface IWowCharacterService
{
	Task<IEnumerable<WowCharacter>> GetAsync();
	Task<WowCharacter> GetByIdAsync(int id);
}
