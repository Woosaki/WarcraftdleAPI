using FluentValidation;
using WarcraftdleAPI.Application.Dto;

namespace WarcraftdleAPI.Application.Validators;

public class CharacterAddRequestValidator : AbstractValidator<CharacterAddRequest>
{
    public CharacterAddRequestValidator()
    {
        RuleFor(x => x.Gender)
            .Must(BeValidGender).WithMessage("Invalid gender value");

		RuleFor(x => x.Class)
			.Must(BeValidClass).WithMessage("Invalid class value");

		RuleFor(x => x.Race)
			.Must(BeValidRace).WithMessage("Invalid race value");

		RuleFor(x => x.Expansions)
			.Must(BeValidExpansion).WithMessage("Invalid expansions value");
	}

    private static readonly List<string> _validGenders = ["Male", "Female"];

	private static readonly List<string> _validClasses = [
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

	private static readonly List<string> _validRaces = [
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

	private static readonly List<string> _validExpansions = [
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

	private bool BeValidGender(string gender) =>
        _validGenders.Contains(gender, StringComparer.OrdinalIgnoreCase);  

    private bool BeValidClass(string? @class) =>
        @class == null || _validClasses.Contains(@class, StringComparer.OrdinalIgnoreCase);

	private bool BeValidRace(string race) =>
		_validRaces.Contains(race, StringComparer.OrdinalIgnoreCase); 

	private bool BeValidExpansion(ICollection<string> expansions) =>
		expansions.All(expansion => _validExpansions.Contains(expansion, StringComparer.OrdinalIgnoreCase));
}
