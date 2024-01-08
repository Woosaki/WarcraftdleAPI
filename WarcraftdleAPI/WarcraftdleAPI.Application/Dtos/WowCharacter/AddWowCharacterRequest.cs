using System.ComponentModel.DataAnnotations;

namespace WarcraftdleAPI.Application.Dtos.WowCharacter;

public record AddWowCharacterRequest
(
	string Name,
	[Url] string Photo,
	string Gender,
	string Race,
	string? Class,
	IEnumerable<string> Expansions,
	IEnumerable<string> Affiliations,
	IEnumerable<string> Zones
);
