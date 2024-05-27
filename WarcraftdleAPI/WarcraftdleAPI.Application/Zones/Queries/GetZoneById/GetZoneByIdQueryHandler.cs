using AutoMapper;
using MediatR;
using System.Net;
using WarcraftdleAPI.Application.Zones.Dtos;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Zones.Queries.GetZoneById;

public class GetZoneByIdQueryHandler(IZonesRepository zonesRepository, IMapper mapper)
    : IRequestHandler<GetZoneByIdQuery, ZoneDto?>
{
    public async Task<ZoneDto?> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
    {
        var zone = await zonesRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Zone with id {request.Id} could not be found", HttpStatusCode.NotFound);
        var zoneDto = mapper.Map<ZoneDto>(zone);

        return zoneDto;
    }
}
