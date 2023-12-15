namespace WarcraftdleAPI.Application.Dto;

public record CharacterAddRequest (
	string Name,
	string Photo,
	string Gender,
	string Race,
	string? Class, 
	ICollection<string> Expansions,
	ICollection<string> Affiliations,
	ICollection<string> Zones
	);;