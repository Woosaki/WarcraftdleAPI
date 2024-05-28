using FluentValidation.TestHelper;
using Moq;
using Xunit;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation.Tests;

public class CreateAffiliationCommandValidatorTests
{
    private readonly CreateAffiliationCommandValidator _validator;
    private readonly Mock<IAffiliationsRepository> _mockAffiliationsRepository;

    public CreateAffiliationCommandValidatorTests()
    {
        _mockAffiliationsRepository = new Mock<IAffiliationsRepository>();
        _validator = new CreateAffiliationCommandValidator(_mockAffiliationsRepository.Object);
    }

    [Fact]
    public void Validator_ForEmptyName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAffiliationCommand { Name = "" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Affiliation name cannot be empty.");
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
        var command = new CreateAffiliationCommand { Name = name };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("Affiliation name can only contain letters or spaces between the words.");
    }

    [Fact]
    public void Validator_ForExistingAffiliation_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAffiliationCommand { Name = "Existing Affiliation" };
        _mockAffiliationsRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(true);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage($"Affiliation 'Existing Affiliation' already exists.");
    }

    [Fact]
    public void Validator_ForNonExistingAffiliation_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateAffiliationCommand { Name = "Affiliation" };
        _mockAffiliationsRepository.Setup(repository => repository.Exists(It.IsAny<string>())).Returns(false);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Name);
    }
}