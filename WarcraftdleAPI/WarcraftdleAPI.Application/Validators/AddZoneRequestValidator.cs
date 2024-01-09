using FluentValidation;
using WarcraftdleAPI.Application.Dtos.Zone;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators;

public class AddZoneRequestValidator : AbstractValidator<AddZoneRequest>
{
    private readonly WarcraftdleDbContext _dbContext;

    public AddZoneRequestValidator(WarcraftdleDbContext dbContext)
    {
        _dbContext = dbContext;

		RuleFor(request => request.Name)
			.NotEmpty()
			.WithMessage("Zone name cannot be empty")

			.Must(ValidationHelpers.BeValidName)
			.WithMessage("Zone name must start with an uppercase letter and contain only letters after that.")

			.Must(name => !_dbContext.Zone.Any(x => x.Name == name))
			.WithMessage(zone => $"Zone '{zone.Name}' already exists");
	}
}
