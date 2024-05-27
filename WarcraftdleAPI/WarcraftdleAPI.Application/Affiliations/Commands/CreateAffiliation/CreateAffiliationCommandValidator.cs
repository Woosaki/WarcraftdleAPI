using FluentValidation;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation;

public class CreateAffiliationCommandValidator : AbstractValidator<CreateAffiliationCommand>
{
    private readonly IAffiliationsRepository _affiliationsRepository;

    public CreateAffiliationCommandValidator(IAffiliationsRepository affiliationsRepository)
    {
        _affiliationsRepository = affiliationsRepository;

        RuleFor(request => request.Name)
           .NotEmpty()
           .WithMessage("Affiliation name cannot be empty.")

           .Matches(@"^[a-zA-Z]+( [a-zA-Z]+)*$")
           .WithMessage("Affiliation name can only contain letters or one space between the words.")

           .Must(name => !_affiliationsRepository.Exists(name))
           .WithMessage(affiliation => $"Affiliation '{affiliation.Name}' already exists.");
    }
}
