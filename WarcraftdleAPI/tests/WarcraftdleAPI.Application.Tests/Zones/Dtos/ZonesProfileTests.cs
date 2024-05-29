using AutoMapper;
using FluentAssertions;
using WarcraftdleAPI.Application.Zones.Commands.CreateZone;
using WarcraftdleAPI.Domain.Entities;
using Xunit;

namespace WarcraftdleAPI.Application.Zones.Dtos.Tests;

public class ZonesProfileTests
{
    private readonly IMapper _mapper;

    public ZonesProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ZonesProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForZoneToZoneDto_MapsCorrectly()
    {
        // Arrange
        var zone = new Zone
        {
            Id = 1,
            Name = "Zone",
            Characters = []
        };

        // Act
        var zoneDto = _mapper.Map<ZoneDto>(zone);

        // Assert
        zoneDto.Should().NotBeNull();
        zoneDto.Id.Should().Be(zone.Id);
        zoneDto.Name.Should().Be(zone.Name);
    }

    [Theory]
    [InlineData("zOnE", "Zone")]
    [InlineData("zone of the and andof", "Zone of the and Andof")]
    [InlineData("ZONE OF THE AND ANDOF", "Zone of the and Andof")]
    public void CreateMap_ForCreateZoneCommandToZone_MapsCorrectly(
        string name, string expected)
    {
        // Arrange
        var command = new CreateZoneCommand
        {
            Name = name
        };

        // Act
        var zone = _mapper.Map<Zone>(command);

        // Assert
        zone.Should().NotBeNull();
        zone.Name.Should().Be(expected);
    }
}