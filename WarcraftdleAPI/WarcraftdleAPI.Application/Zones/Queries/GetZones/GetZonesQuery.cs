using MediatR;
using WarcraftdleAPI.Application.Zones.Dtos;

namespace WarcraftdleAPI.Application.Zones.Queries.GetZones;

public class GetZonesQuery : IRequest<IEnumerable<ZoneDto>>
{
}
