using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Zones.Commands.DeleteZone;

public class DeleteZoneCommandHandler(
    IZonesRepository zonesRepository,
    ILogger<DeleteZoneCommandHandler> logger)
    : IRequestHandler<DeleteZoneCommand>
{
    public async Task Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting zone with ID: {Id}", request.Id);

        var zone = await zonesRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Zone with id {request.Id} could not be found", HttpStatusCode.NotFound);

        await zonesRepository.DeleteAsync(zone);
    }
}
