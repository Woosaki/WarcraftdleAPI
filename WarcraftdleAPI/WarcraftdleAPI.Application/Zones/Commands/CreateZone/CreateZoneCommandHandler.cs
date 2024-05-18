using AutoMapper;
using MediatR;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Application.Zones.Commands.CreateZone;

public class CreateZoneCommandHandler(IZonesRepository zonesRepository, IMapper mapper) : IRequestHandler<CreateZoneCommand, int>
{
    public async Task<int> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
    {
        var zone = mapper.Map<Zone>(request);
        int id = await zonesRepository.CreateAsync(zone);

        return id;
    }
}
