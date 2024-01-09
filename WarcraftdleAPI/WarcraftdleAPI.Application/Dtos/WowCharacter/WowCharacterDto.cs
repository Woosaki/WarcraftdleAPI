namespace WarcraftdleAPI.Application.Dtos.WowCharacter;

public record WowCharacterDto
(
	int Id,
	string Name,
	string Photo,
	string Gender,
	string Race,
	string? Class,
	IEnumerable<string> Expansions,
	IEnumerable<string> Affiliations,
	IEnumerable<string> Zones
);
