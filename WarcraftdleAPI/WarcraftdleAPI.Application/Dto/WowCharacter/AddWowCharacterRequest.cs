using System.ComponentModel.DataAnnotations;

namespace WarcraftdleAPI.Application.Dto.WowCharacter;

public record AddWowCharacterRequest
(
	[Required] string Name,
	[Required] string Photo,
	[Required] string Gender,
	[Required] string Race,
	string? Class,
	[Required] IEnumerable<string> Expansions,
	[Required] IEnumerable<string> Affiliations,
	[Required] IEnumerable<string> Zones
);
