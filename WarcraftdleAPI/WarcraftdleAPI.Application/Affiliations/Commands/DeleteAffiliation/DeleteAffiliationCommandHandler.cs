using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Commands.DeleteAffiliation;

public class DeleteAffiliationCommandHandler(
    IAffiliationsRepository affiliationsRepository,
    ILogger<DeleteAffiliationCommandHandler> logger)
    : IRequestHandler<DeleteAffiliationCommand>
{
    public async Task Handle(DeleteAffiliationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting affiliation with ID: {Id}", request.Id);

        var affiliation = await affiliationsRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Affiliation with id {request.Id} could not be found", HttpStatusCode.NotFound);

        await affiliationsRepository.DeleteAsync(affiliation);
    }
}
