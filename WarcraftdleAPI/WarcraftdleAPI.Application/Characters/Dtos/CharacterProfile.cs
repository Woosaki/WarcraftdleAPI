using AutoMapper;
using WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;
using WarcraftdleAPI.Domain.Entities;

namespace WarcraftdleAPI.Application.Characters.Dtos;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<Character, CharacterDto>();
        CreateMap<CreateCharacterCommand, Character>();
    }
}
