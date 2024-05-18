using MediatR;

namespace WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation;

public class CreateAffiliationCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}
