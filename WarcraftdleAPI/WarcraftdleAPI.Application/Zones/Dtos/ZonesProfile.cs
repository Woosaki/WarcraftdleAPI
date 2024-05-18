using AutoMapper;
using System.Globalization;
using WarcraftdleAPI.Application.Zones.Commands.CreateZone;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Application.Zones.Dtos;

public class ZonesProfile : Profile
{
    public ZonesProfile()
    {
        CreateMap<Zone, ZoneDto>();
        CreateMap<CreateZoneCommand, Zone>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => ToTitleCase(src.Name)));
    }

    private static string ToTitleCase(string name)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
    }
}
