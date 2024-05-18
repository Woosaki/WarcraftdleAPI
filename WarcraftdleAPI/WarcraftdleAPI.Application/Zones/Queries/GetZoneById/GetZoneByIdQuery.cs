using MediatR;
using WarcraftdleAPI.Application.Zones.Dtos;

namespace WarcraftdleAPI.Application.Zones.Queries.GetZoneById;

public class GetZoneByIdQuery(int id) : IRequest<ZoneDto?>
{
    public int Id { get; } = id;
}
