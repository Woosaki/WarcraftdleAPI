using MediatR;

namespace WarcraftdleAPI.Application.Zones.Commands.DeleteZone;

public class DeleteZoneCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
