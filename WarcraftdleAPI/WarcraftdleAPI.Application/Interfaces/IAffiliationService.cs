using WarcraftdleAPI.Domain.Character;

namespace WarcraftdleAPI.Application.Interfaces;

public interface IAffiliationService
{
	Task<Affiliation?> GetByNameAsync(string name);
	Task AddAsync(string name);
}
