using MediatR;

namespace WarcraftdleAPI.Application.Affiliations.Commands.DeleteAffiliation;

public class DeleteAffiliationCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
