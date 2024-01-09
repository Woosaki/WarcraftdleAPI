using FluentValidation;
using System.Xml.Linq;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators;

public class AddZoneRequestValidator : AbstractValidator<string>
{
    private readonly WarcraftdleDbContext _dbContext;

    public AddZoneRequestValidator(WarcraftdleDbContext dbContext)
    {
        _dbContext = dbContext;

		RuleFor(request => request)
			.NotEmpty()
			.WithMessage("Zone name cannot be empty")

			.Must(ValidationHelpers.BeValidName)
			.WithMessage("Zone name must start with an uppercase letter and contain only letters after that.")

			.Must(name => !_dbContext.Zone.Any(x => x.Name == name))
			.WithMessage(zoneName => $"Zone '{zoneName}' already exists");
	}
}
