using FluentValidation;
using WarcraftdleAPI.Application.Dtos.Affiliations;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators.Affiliations;

public class AddAffiliationRequestValidator : AbstractValidator<AddAffiliationRequest>
{
    private readonly WarcraftdleDbContext _dbContext;

    public AddAffiliationRequestValidator(WarcraftdleDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Affiliation name cannot be empty")

            .Must(ValidationHelpers.BeValidName)
            .WithMessage("Affiliation name must start with an uppercase letter and contain only letters after that.")

            .Must(name => !_dbContext.Affiliation.Any(x => x.Name == name))
            .WithMessage(affiliationName => $"Affiliation '{affiliationName}' already exists");
    }
}
