using FluentValidation.TestHelper;
using Moq;
using Xunit;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Zones.Commands.CreateZone.Tests;

public class CreateZoneCommandValidatorTests
{
    private readonly CreateZoneCommandValidator _validator;
    private readonly Mock<IZonesRepository> _mockZonesRepository;

    public CreateZoneCommandValidatorTests()
    {
        _mockZonesRepository = new Mock<IZonesRepository>();
        _validator = new CreateZoneCommandValidator(_mockZonesRepository.Object);
    }

    [Fact]
    public void Validator_ForEmptyName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateZoneCommand { Name = "" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Zone name cannot be empty.");
    }

    [Theory]
    [InlineData("invalid name1")]
    [InlineData("invalid-name")]
    [InlineData("invalid  name")]
    [InlineData("invalid name ")]
    [InlineData(" invalid name")]
    public void Validator_ForInvalidName_ShouldHaveValidationError(string name)
    {
        // Arrange
        var command = new CreateZoneCommand { Name = name };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Zone name can only contain letters or spaces between the words.");
    }

    [Fact]
    public void Validator_ForExistingZone_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateZoneCommand { Name = "Existing Zone" };
        _mockZonesRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(true);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage($"Zone 'Existing Zone' already exists.");
    }

    [Fact]
    public void Validator_ForNonExistingZone_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateZoneCommand { Name = "Zone" };
        _mockZonesRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(false);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Name);
    }
}