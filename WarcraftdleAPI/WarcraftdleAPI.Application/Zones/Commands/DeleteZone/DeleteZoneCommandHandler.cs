﻿using MediatR;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Zones.Commands.DeleteZone;

public class DeleteZoneCommandHandler(IZonesRepository zonesRepository) : IRequestHandler<DeleteZoneCommand>
{
    public async Task Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
    {
        var zone = await zonesRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Zone with id {request.Id} could not be found", HttpStatusCode.NotFound);

        await zonesRepository.DeleteAsync(zone);
    }
}
