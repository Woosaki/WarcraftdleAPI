using FluentValidation;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Enums;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
{
    private readonly ICharactersRepository _charactersRepository;
    private readonly IAffiliationsRepository _affiliationsRepository;
    private readonly IZonesRepository _zonesRepository;

    public CreateCharacterCommandValidator(
        ICharactersRepository charactersRepository,
        IAffiliationsRepository affiliationsRepository,
        IZonesRepository zonesRepository)
    {
        _charactersRepository = charactersRepository;
        _affiliationsRepository = affiliationsRepository;
        _zonesRepository = zonesRepository;

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} field cannot be empty")

            .Matches(@"^([A-Z][a-z]*(\s|$))*[A-Z][a-z]*$")
            .WithMessage("Each word in {PropertyName} must start with an uppercase letter and contain only lowercase letters after that")

            .Must(name => !_charactersRepository.ExistsWithName(name))
            .WithMessage(request => $"Character with name '{request.Name}' already exists");

        RuleFor(request => request.Gender)
            .Must(_validGenderNames.Contains)
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");

        RuleFor(request => request.Race)
            .Must(_validRaceNames.Contains)
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");

        RuleFor(request => request.Class)
            .Must(_validClassNames.Contains)
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");

        // Expansion rules
        RuleFor(request => request.Expansions)
            .Must(expansions => expansions.Any())
            .WithMessage("{PropertyName} must contain at least one item")

            .Must(expansions => expansions.Count() <= 3)
            .WithMessage("{PropertyName} cannot have more than 3 items")

            .Must(HaveNoDuplicates)
            .WithMessage("{PropertyName} cannot have duplicate items");

        RuleForEach(request => request.Expansions)
            .Must(_validExpansionNames.Contains)
            .WithMessage("Invalid Expansion: {PropertyValue}");

        //Affiliation rules
        RuleFor(request => request.Affiliations)
            .Must(affiliations => affiliations.Any())
            .WithMessage("{PropertyName} must contain at least one item")

            .Must(affiliations => affiliations.Count() <= 3)
            .WithMessage("{PropertyName} cannot have more than 3 items")

            .Must(HaveNoDuplicates)
            .WithMessage("{PropertyName} cannot have duplicate items");

        RuleForEach(request => request.Affiliations)
            .Must(ExistInDatabase<Affiliation>)
            .WithMessage("Affiliation not found: {PropertyValue}");

        //Zone rules
        RuleFor(request => request.Zones)
            .Must(zones => zones.Any())
            .WithMessage("{PropertyName} must contain at least one item")

            .Must(zones => zones.Count() <= 3)
            .WithMessage("{PropertyName} cannot have more than 3 items")

            .Must(HaveNoDuplicates)
            .WithMessage("{PropertyName} cannot have duplicate items");

        RuleForEach(request => request.Zones)
            .Must(ExistInDatabase<Zone>)
            .WithMessage("Zone not found: {PropertyValue}");
    }

    private static readonly string[] _validClassNames = Enum.GetNames<Class>();

    private static readonly string[] _validGenderNames = Enum.GetNames<Gender>();

    private static readonly string[] _validRaceNames = Enum.GetNames<Race>();

    private static readonly string[] _validExpansionNames = Enum.GetNames<Expansion>();

    private bool HaveNoDuplicates(IEnumerable<string> strings)
    {
        return strings.Distinct().Count() == strings.Count();
    }

    private bool ExistInDatabase<T>(string value) where T : class
    {
        if (typeof(T) == typeof(Affiliation))
        {
            return _affiliationsRepository.Exists(value);
        }

        if (typeof(T) == typeof(Zone))
        {
            return _zonesRepository.Exists(value);
        }

        return false;
    }
}
