using FluentValidation;
using WarcraftdleAPI.Application.Dtos.Affiliations;
using WarcraftdleAPI.Infrastructure;

namespace WarcraftdleAPI.Application.Validators.Affiliations;

public class AddMultipleAffiliationRequestValidator : AbstractValidator<AddMultipleAffiliationRequest>
{
    public AddMultipleAffiliationRequestValidator(WarcraftdleDbContext dbContext)
    {
        RuleFor(request => request.AffiliationNames)
            .NotEmpty()
            .WithMessage("You have to provide at least one name")

            .Must(names => !dbContext.Affiliation.Any(x => names.Contains(x.Name)))
            .WithMessage(request => $"One or more affiliations already exist");

        RuleForEach(request => request.AffiliationNames)
            .NotEmpty()
            .WithMessage("Affiliation name cannot be empty")

            .Must(ValidationHelpers.BeValidName)
            .WithMessage("Affiliation name must start with an uppercase letter and contain only letters after that.");
    }
}