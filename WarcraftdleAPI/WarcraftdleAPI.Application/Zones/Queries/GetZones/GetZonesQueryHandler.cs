using AutoMapper;
using MediatR;
using WarcraftdleAPI.Application.Zones.Dtos;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Zones.Queries.GetZones;

public class GetZonesQueryHandler(IZonesRepository zonesRepository, IMapper mapper)
    : IRequestHandler<GetZonesQuery, IEnumerable<ZoneDto>>
{
    public async Task<IEnumerable<ZoneDto>> Handle(GetZonesQuery request, CancellationToken cancellationToken)
    {
        var zones = await zonesRepository.GetAsync();
        var zoneDtos = mapper.Map<IEnumerable<ZoneDto>>(zones);

        return zoneDtos;
    }
}
