using AutoMapper;
using MediatR;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace WarcraftdleAPI.Application.Zones.Commands.CreateZone;

public class CreateZoneCommandHandler(
    IZonesRepository zonesRepository,
    ILogger<CreateZoneCommandHandler> logger,
    IMapper mapper)
    : IRequestHandler<CreateZoneCommand, int>
{
    public async Task<int> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new zone {Zone}", request.Name);

        var zone = mapper.Map<Zone>(request);
        int id = await zonesRepository.CreateAsync(zone);

        return id;
    }
}
