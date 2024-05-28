using FluentValidation;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Zones.Commands.CreateZone;

public class CreateZoneCommandValidator : AbstractValidator<CreateZoneCommand>
{
    private readonly IZonesRepository _zonesRepository;

    public CreateZoneCommandValidator(IZonesRepository zonesRepository)
    {
        _zonesRepository = zonesRepository;

        RuleFor(request => request.Name)
           .NotEmpty()
           .WithMessage("Zone name cannot be empty.")

           .Matches(@"^[a-zA-Z]+( [a-zA-Z]+)*$")
           .WithMessage("Zone name can only contain letters or spaces between the words.")

           .Must(name => !_zonesRepository.Exists(name))
           .WithMessage(zone => $"Zone '{zone.Name}' already exists.");
    }
}
