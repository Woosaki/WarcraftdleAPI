using MediatR;

namespace WarcraftdleAPI.Application.Zones.Commands.CreateZone;

public class CreateZoneCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}
