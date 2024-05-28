using AutoMapper;
using FluentAssertions;
using WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Enums;
using Xunit;

namespace WarcraftdleAPI.Application.Characters.Dtos.Tests;

public class CharacterProfileTests
{
    private readonly IMapper _mapper;

    public CharacterProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CharacterProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForCharacterToCharacterDto_MapsCorrectly()
    {
        // Arrange
        var character = new Character
        {
            Id = 1,
            Name = "Character",
            Gender = Gender.Male,
            Race = Race.Human,
            Class = Class.Warrior,
            Expansions =
            [
                Expansion.Classic,
                Expansion.TBC,
                Expansion.Cata
            ],
            Affiliations =
            [
                new Affiliation { Id = 1, Name = "Alliance" },
                new Affiliation { Id = 2, Name = "Horde" }
            ],
            Zones =
            [
                new Zone { Id = 1, Name = "Elwynn Forest" },
                new Zone { Id = 2, Name = "Durotar" }
            ]
        };

        // Act
        var characterDto = _mapper.Map<CharacterDto>(character);

        // Assert
        characterDto.Should().NotBeNull();
        characterDto.Id.Should().Be(character.Id);
        characterDto.Name.Should().Be(character.Name);
        characterDto.Gender.Should().Be(character.Gender.ToString());
        characterDto.Race.Should().Be(character.Race.ToString());
        characterDto.Class.Should().NotBeNull();
        characterDto.Class.Should().Be(character.Class.ToString());

        characterDto.Expansions.Should().BeEquivalentTo(
            character.Expansions.Select(e => e.ToString()));
        characterDto.Affiliations.Should().BeEquivalentTo(
            character.Affiliations.Select(a => a.Name));
        characterDto.Zones.Should().BeEquivalentTo(
            character.Zones.Select(z => z.Name));
    }

    [Fact]
    public void CreateMap_ForCharacterToCharacterDto_WithNullClass_MapsCorrectly()
    {
        // Arrange
        var character = new Character
        {
            Class = null
        };

        // Act
        var characterDto = _mapper.Map<CharacterDto>(character);

        // Assert
        characterDto.Class.Should().BeNull();
    }

    [Fact]
    public void CreateMap_ForCreateCharacterCommandToCharacter_MapsCorrectly()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Name = "Character",
            Gender = "Male",
            Race = "Human",
            Class = "Warrior",
            Expansions = ["Classic", "TBC", "Cata"],
            Affiliations = ["Alliance", "Horde"],
            Zones = ["Elwynn Forest", "Durotar"]
        };

        // Act
        var character = _mapper.Map<Character>(command);

        // Assert
        character.Should().NotBeNull();
        character.Name.Should().Be(command.Name);
        character.Gender.Should().Be(Gender.Male);
        character.Race.Should().Be(Race.Human);
        character.Class.Should().NotBeNull();
        character.Class.Should().Be(Class.Warrior);

        character.Expansions.Should().BeEquivalentTo(
            command.Expansions.Select(Enum.Parse<Expansion>));
        character.Affiliations.Should().BeNull();
        character.Zones.Should().BeNull();
    }

    [Fact]
    public void CreateMap_ForCreateCharacterCommandToCharacter_WithNullClass_MapsCorrectly()
    {
        // Arrange
        var command = new CreateCharacterCommand
        {
            Class = null
        };

        // Act
        var character = _mapper.Map<Character>(command);

        // Assert
        character.Class.Should().BeNull();
    }
}