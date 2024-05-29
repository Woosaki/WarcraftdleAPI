using AutoMapper;
using FluentAssertions;
using WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation;
using WarcraftdleAPI.Domain.Entities;
using Xunit;

namespace WarcraftdleAPI.Application.Affiliations.Dtos.Tests;

public class AffiliationsProfileTests
{
    private readonly IMapper _mapper;

    public AffiliationsProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AffiliationsProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForAffiliationToAffiliationeDto_MapsCorrectly()
    {
        // Arrange
        var affiliation = new Affiliation
        {
            Id = 1,
            Name = "Affiliation",
            Characters = []
        };

        // Act
        var affiliationDto = _mapper.Map<AffiliationDto>(affiliation);

        // Assert
        affiliationDto.Should().NotBeNull();
        affiliationDto.Id.Should().Be(affiliation.Id);
        affiliationDto.Name.Should().Be(affiliation.Name);
    }

    [Theory]
    [InlineData("aFFiliaTioN", "Affiliation")]
    [InlineData("affiliation of the and andof", "Affiliation of the and Andof")]
    [InlineData("AFFILIATION OF THE AND ANDOF", "Affiliation of the and Andof")]
    public void CreateMap_ForCreateAffiliationCommandToAffiliation_MapsCorrectly(
        string name, string expected)
    {
        // Arrange
        var command = new CreateAffiliationCommand
        {
            Name = name
        };

        // Act
        var affiliation = _mapper.Map<Affiliation>(command);

        // Assert
        affiliation.Should().NotBeNull();
        affiliation.Name.Should().Be(expected);
    }
}