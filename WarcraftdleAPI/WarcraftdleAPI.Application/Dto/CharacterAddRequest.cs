using System.ComponentModel.DataAnnotations;

namespace WarcraftdleAPI.Application.Dto;

public record CharacterAddRequest (
	string Name,
	[Url] string Photo,
	string Gender,
	string Race,
	string? Class, 
	ICollection<string> Expansions,
	ICollection<string> Affiliations,
	ICollection<string> Zones
	);