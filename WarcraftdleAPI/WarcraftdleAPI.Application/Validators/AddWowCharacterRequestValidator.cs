using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Dtos.WowCharacter;
using WarcraftdleAPI.Domain.WowCharacter;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators;

public class AddWowCharacterRequestValidator : AbstractValidator<AddWowCharacterRequest>
{
	private readonly WarcraftdleDbContext _dbContext;

	public AddWowCharacterRequestValidator(WarcraftdleDbContext dbContext)
    {
		_dbContext = dbContext;

		RuleFor(request => request.Gender)
			.Must(name => _validGenderNames.Contains(name))
			.WithMessage("Invalid gender: {PropertyValue}");

		RuleFor(request => request.Race)
			.Must(name => _validRaceNames.Contains(name))
			.WithMessage("Invalid race: {PropertyValue}");

		RuleFor(request => request.Class)
			.Must(name => _validClassNames.Contains(name))
			.WithMessage("Invalid class: {PropertyValue}");

		RuleForEach(request => request.Expansions)
			.Must(name => _validExpansionNames.Contains(name))
			.WithMessage("Invalid expansion: {PropertyValue}");

		RuleForEach(request => request.Affiliations)
			.Must(ExistInDatabase<Affiliation>)
			.WithMessage("Affiliation not found: {PropertyValue}");

		RuleForEach(request => request.Zones)
			.Must(ExistInDatabase<Zone>)
			.WithMessage("Zone not found: {PropertyValue}");
	}

	private static readonly string[] _validGenderNames = [
		"Male",
		"Female"
	];

	private static readonly string[] _validRaceNames = [
		"Human",
		"Dwarf",
		"Night Elf",
		"Gnome",
		"Draenei",
		"Worgen",
		"Pandaren",
		"Orc",
		"Undead",
		"Tauren",
		"Troll",
		"Blood Elf",
		"Goblin",
		"Dragon"
	];

	private static readonly string[] _validClassNames = [
		"Warrior",
		"Paladin",
		"Hunter",
		"Rogue",
		"Priest",
		"Shaman",
		"Mage",
		"Warlock",
		"Monk",
		"Druid",
		"Demon Hunter",
		"Death Knight",
		"Evoker"
	];

	private static readonly string[] _validExpansionNames = [
		"Classic",
		"The Burning Crusade",
		"Wrath of the Lich King",
		"Cataclysm",
		"Mists of Pandaria",
		"Warlords of Draenor",
		"Legion",
		"Battle for Azeroth",
		"Shadowlands",
		"Dragonflight"
	];

	private bool ExistInDatabase<T>(string name) where T : class
	{
		var existingNames =  GetExistingNames<T>();
		return existingNames.Contains(name);
	}

	private IEnumerable<string> GetExistingNames<T>() where T : class
	{
		return _dbContext.Set<T>()
			.Select(x => (string)x.GetType().GetProperty("Name")!.GetValue(x)!)
			.AsEnumerable();
	}
}
