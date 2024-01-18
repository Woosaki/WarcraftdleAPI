using FluentValidation;
using WarcraftdleAPI.Application.Dtos.Zones;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators.Zones;

public class AddMultipleZoneRequestValidator : AbstractValidator<AddMultipleZoneRequest>
{
    public AddMultipleZoneRequestValidator(WarcraftdleDbContext dbContext)
    {
        RuleFor(request => request.ZoneNames)
            .NotEmpty()
            .WithMessage("You have to provide at least one name")

            .Must(names => !dbContext.Zone.Any(x => names.Contains(x.Name)))
            .WithMessage(request => $"One or more zones already exist");

        RuleForEach(request => request.ZoneNames)
            .NotEmpty()
            .WithMessage("Zone name cannot be empty")

            .Must(ValidationHelpers.BeValidName)
            .WithMessage("Zone name must start with an uppercase letter and contain only letters after that.");
    }
}