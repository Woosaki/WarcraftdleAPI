using AutoMapper;
using System.Globalization;
using WarcraftdleAPI.Application.Zones.Commands.CreateZone;
using WarcraftdleAPI.Domain.Entities;

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
        var words = name.ToLower().Split(' ');
        var wordsToSkip = new HashSet<string> { "of", "the", "and" };

        for (int i = 0; i < words.Length; i++)
        {
            if (i == 0 || !wordsToSkip.Contains(words[i]))
            {
                words[i] = char.ToUpper(words[i][0]) + words[i][1..];
            }
        }

        return string.Join(' ', words);
    }
}
