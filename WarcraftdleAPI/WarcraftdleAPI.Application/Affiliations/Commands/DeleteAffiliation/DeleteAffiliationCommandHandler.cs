using MediatR;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Commands.DeleteAffiliation;

public class DeleteAffiliationCommandHandler(IAffiliationsRepository affiliationsRepository)
    : IRequestHandler<DeleteAffiliationCommand>
{
    public async Task Handle(DeleteAffiliationCommand request, CancellationToken cancellationToken)
    {
        var affiliation = await affiliationsRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Affiliation with id {request.Id} could not be found", HttpStatusCode.NotFound);

        await affiliationsRepository.DeleteAsync(affiliation);
    }
}
