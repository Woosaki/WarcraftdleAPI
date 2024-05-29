using FluentValidation.TestHelper;
using Moq;
using WarcraftdleAPI.Domain.Repositories;
using Xunit;

namespace WarcraftdleAPI.Application.Characters.Commands.CreateCharacter.Tests;

public class CreateCharacterCommandValidatorTests
{
    private readonly CreateCharacterCommandValidator _validator;
    private readonly Mock<ICharactersRepository> _mockCharactersRepository;
    private readonly Mock<IAffiliationsRepository> _mockAffiliationsRepository;
    private readonly Mock<IZonesRepository> _mockZonesRepository;

    public CreateCharacterCommandValidatorTests()
    {
        _mockCharactersRepository = new Mock<ICharactersRepository>();
        _mockAffiliationsRepository = new Mock<IAffiliationsRepository>();
        _mockZonesRepository = new Mock<IZonesRepository>();
        _validator = new CreateCharacterCommandValidator(
            _mockCharactersRepository.Object,
            _mockAffiliationsRepository.Object,
            _mockZonesRepository.Object);
    }

    [Fact]
    public void Validator_ForEmptyName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Name = "",
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Name field cannot be empty");
    }

    [Fact]
    public void Validator_ForExistingCharacterName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Name = "Existing Character",
            Expansions = [],
            Affiliations = [],
            Zones = []
        };
        _mockCharactersRepository.Setup(repository => repository.ExistsWithName(It.IsAny<string>())).Returns(true);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Character with name 'Existing Character' already exists");
    }


    [Fact]
    public void Validator_ForNonExistingCharacterName_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Name = "Character",
            Expansions = [],
            Affiliations = [],
            Zones = []
        };
        _mockCharactersRepository.Setup(repository => repository.ExistsWithName(It.IsAny<string>())).Returns(false);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Name);
    }

    [Theory]
    [InlineData("invalid name")]
    [InlineData("Invalid Name1")]
    [InlineData("Invalid-name")]
    [InlineData("Invalid  Name")]
    [InlineData("Invalid Name ")]
    [InlineData(" Invalid Name")]
    [InlineData("INVALID NAME")]
    [InlineData("InvalidName")]
    public void Validator_ForInvalidName_ShouldHaveValidationError(string name)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Name = name,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Each word in Name must start with an uppercase letter and contain only lowercase letters after that");
    }

    [Theory]
    [InlineData("Male")]
    [InlineData("Female")]
    public void Validator_ForValidGender_ShouldNotHaveValidationError(string gender)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Gender = gender,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Gender);
    }

    [Theory]
    [InlineData("male")]
    [InlineData("female")]
    [InlineData("feMale")]
    [InlineData("Other")]
    public void Validator_ForInvalidGender_ShouldHaveValidationError(string gender)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Gender = gender,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Gender)
            .WithErrorMessage($"Invalid Gender: {gender}");
    }

    [Theory]
    [InlineData("Human")]
    [InlineData("Dwarf")]
    [InlineData("NightElf")]
    [InlineData("Gnome")]
    [InlineData("Draenei")]
    [InlineData("Worgen")]
    [InlineData("Pandaren")]
    [InlineData("Orc")]
    [InlineData("Undead")]
    [InlineData("Tauren")]
    [InlineData("Troll")]
    [InlineData("BloodElf")]
    [InlineData("Goblin")]
    [InlineData("Dragon")]
    public void Validator_ForValidRace_ShouldNotHaveValidationError(string race)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Race = race,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Race);
    }

    [Theory]
    [InlineData("nightelf")]
    [InlineData("nightElf")]
    [InlineData("Nightelf")]
    [InlineData("NIGHTELF")]
    [InlineData("Other")]
    public void Validator_ForInvalidRace_ShouldHaveValidationError(string race)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Race = race,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Race)
            .WithErrorMessage($"Invalid Race: {race}");
    }

    [Theory]
    [InlineData("Warrior")]
    [InlineData("Paladin")]
    [InlineData("Hunter")]
    [InlineData("Rogue")]
    [InlineData("Priest")]
    [InlineData("Shaman")]
    [InlineData("Mage")]
    [InlineData("Warlock")]
    [InlineData("Monk")]
    [InlineData("Druid")]
    [InlineData("DemonHunter")]
    [InlineData("DeathKnight")]
    [InlineData("Evoker")]
    public void Validator_ForValidClass_ShouldNotHaveValidationError(string @class)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Class = @class,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Class);
    }

    [Theory]
    [InlineData("Demonhunter")]
    [InlineData("demonhunter")]
    [InlineData("demonHunter")]
    [InlineData("DEMONHUNTER")]
    [InlineData("Other")]
    public void Validator_ForInvalidClass_ShouldHaveValidationError(string @class)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Class = @class,
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Class)
            .WithErrorMessage($"Invalid Class: {@class}");
    }

    [Fact]
    public void Validator_ForEmptyExpansionsCollection_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Expansions)
            .WithErrorMessage("Expansions must contain at least one item");
    }

    [Fact]
    public void Validator_ForTooManyExpansions_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions =
            [
                "Expansion1",
                "Expansion2",
                "Expansion3",
                "Expansion4"
            ],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Expansions)
            .WithErrorMessage("Expansions cannot have more than 3 items");
    }

    [Fact]
    public void Validator_ForDuplicateExpansions_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = ["Expansion", "Expansion"],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Expansions)
            .WithErrorMessage("Expansions cannot have duplicate items");
    }

    [Theory]
    [InlineData("Classic")]
    [InlineData("TBC")]
    [InlineData("WotLK")]
    [InlineData("Cata")]
    [InlineData("MOP")]
    [InlineData("WOD")]
    [InlineData("Legion")]
    [InlineData("BFA")]
    [InlineData("SL")]
    [InlineData("DF")]
    public void Validator_ForValidExpansion_ShouldNotHaveValidationError(string expansion)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [expansion],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Expansions);
    }

    [Theory]
    [InlineData("wotlk")]
    [InlineData("WOTLK")]
    [InlineData("wotLK")]
    [InlineData("Wotlk")]
    [InlineData("Other")]
    public void Validator_ForInvalidExpansion_ShouldHaveValidationError(string expansion)
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = ["Classic", expansion, "TBC"],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Expansions)
            .WithErrorMessage($"Invalid Expansion: {expansion}");
    }

    [Fact]
    public void Validator_ForEmptyAffiliationsCollection_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Affiliations)
            .WithErrorMessage("Affiliations must contain at least one item");
    }

    [Fact]
    public void Validator_ForTooManyAffiliations_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations =
            [
                "Affiliation1",
                "Affiliation2",
                "Affiliation3",
                "Affiliation4"
            ],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Affiliations)
            .WithErrorMessage("Affiliations cannot have more than 3 items");
    }

    [Fact]
    public void Validator_ForDuplicateAffiliations_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = ["Affiliation", "Affiliation"],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Affiliations)
            .WithErrorMessage("Affiliations cannot have duplicate items");
    }

    [Fact]
    public void Validator_ForExistingAffiliation_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = ["Existing Affiliation"],
            Zones = []
        };
        _mockAffiliationsRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(true);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Affiliations);
    }

    [Fact]
    public void Validator_ForNonExistingAffiliation_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = ["Non Existing Affiliation"],
            Zones = []
        };
        _mockAffiliationsRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(false);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Affiliations)
            .WithErrorMessage("Affiliation not found: Non Existing Affiliation");
    }

    [Fact]
    public void Validator_ForEmptyZonesCollection_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones = []
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Zones)
            .WithErrorMessage("Zones must contain at least one item");
    }

    [Fact]
    public void Validator_ForTooManyZones_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones =
            [
                "Zone1",
                "Zone2",
                "Zone3",
                "Zone4"
            ]
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Zones)
            .WithErrorMessage("Zones cannot have more than 3 items");
    }

    [Fact]
    public void Validator_ForDuplicateZones_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones = ["Zone", "Zone"]
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Zones)
            .WithErrorMessage("Zones cannot have duplicate items");
    }

    [Fact]
    public void Validator_ForExistingZone_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones = ["Existing Zone"]
        };
        _mockZonesRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(true);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Zones);
    }

    [Fact]
    public void Validator_ForNonExistingZone_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Expansions = [],
            Affiliations = [],
            Zones = ["Non Existing Zone"]
        };
        _mockZonesRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(false);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Zones)
            .WithErrorMessage("Zone not found: Non Existing Zone");
    }
}