using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WarcraftdleAPI.Application.Dtos.WowCharacters;
using WarcraftdleAPI.Domain.WowCharacter;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators.WowCharacters;

public class AddWowCharacterRequestValidator : AbstractValidator<AddWowCharacterRequest>
{
    private readonly WarcraftdleDbContext _dbContext;

    public AddWowCharacterRequestValidator(WarcraftdleDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} field cannot be empty")

            .Must(ValidationHelpers.BeValidName)
            .WithMessage("{PropertyName} must start with an uppercase letter and contain only letters after that.")

            .Must(BeUnique)
            .WithMessage(request => $"Character with name '{request.Name}' already exists");

        RuleFor(request => request.Gender)
            .Must(gender => _validGenderNames.Contains(gender))
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");

        RuleFor(request => request.Race)
            .Must(race => _validRaceNames.Contains(race))
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");

        RuleFor(request => request.Class)
            .Must(BeValidClass)
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");

        RuleFor(request => request.Expansions)
            .Must(expansions => expansions.Any())
            .WithMessage("{PropertyName} must contain at least one item")

            .Must(expansions => expansions.Count() <= 3)
			.WithMessage("{PropertyName} cannot have more than 3 items");

		RuleForEach(request => request.Expansions)
            .Must(expansion => _validExpansionNames.Contains(expansion))
            .WithMessage("Invalid Expansion: {PropertyValue}");

        RuleFor(request => request.Affiliations)
            .Must(affiliations => affiliations.Any())
            .WithMessage("{PropertyName} must contain at least one item")

			.Must(affiliations => affiliations.Count() <= 3)
			.WithMessage("{PropertyName} cannot have more than 3 items");

		RuleForEach(request => request.Affiliations)
            .Must(ExistInDatabase<Affiliation>)
            .WithMessage("Affiliation not found: {PropertyValue}");

        RuleFor(request => request.Zones)
            .Must(zones => zones.Any())
            .WithMessage("{PropertyName} must contain at least one item");

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

    private bool BeUnique(string name)
    {
        return !_dbContext.WowCharacter.Any(x => x.Name == name);
    }

    private bool BeValidClass(string? @class)
    {
        if (@class == null)
        {
            return true;
        }

        return !string.IsNullOrWhiteSpace(@class) && _validClassNames.Contains(@class);
    }

    private bool ExistInDatabase<T>(string name) where T : class
    {
        var existingNames = GetExistingNames<T>();
        return existingNames.Contains(name);
    }

    private IEnumerable<string> GetExistingNames<T>() where T : class
    {
        return _dbContext.Set<T>()
            .Select(x => (string)x.GetType().GetProperty("Name")!.GetValue(x)!)
            .AsEnumerable();
    }
}
