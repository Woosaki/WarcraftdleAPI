﻿using AutoMapper;
using WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Enums;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Dtos;

public class CharacterProfile : Profile
{
    private readonly IAffiliationsRepository _affiliationsRepository;
    private readonly IZonesRepository _zonesRepository;

    public CharacterProfile(IAffiliationsRepository affiliationsRepository, IZonesRepository zonesRepository)
    {
        _affiliationsRepository = affiliationsRepository;
        _zonesRepository = zonesRepository;

        CreateMap<Character, CharacterDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(
                src => src.Gender.ToString()))
            .ForMember(dest => dest.Race, opt => opt.MapFrom(
                src => src.Race.ToString()))
            .ForMember(dest => dest.Class, opt => opt.MapFrom(
                src => src.Class.HasValue ? src.Class.ToString() : null))
            .ForMember(dest => dest.Expansions, opt => opt.MapFrom(        
                src => src.Expansions.Select(e => e.ToString())))
            .ForMember(dest => dest.Affiliations, opt => opt.MapFrom(
                src => src.Affiliations.Select(a => a.Name)))
            .ForMember(dest => dest.Zones, opt => opt.MapFrom(
                src => src.Zones.Select(z => z.Name)));

        CreateMap<CreateCharacterCommand, Character>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(
                src => Enum.Parse<Gender>(src.Gender)))
            .ForMember(dest => dest.Race, opt => opt.MapFrom(
                src => Enum.Parse<Race>(src.Race)))
            .ForMember(dest => dest.Class, opt => opt.MapFrom(
                src => src.Class != null ? Enum.Parse<Class>(src.Class) : (Class?)null))
            .ForMember(dest => dest.Expansions, opt => opt.MapFrom(
                src => src.Expansions.Select(e => Enum.Parse<Expansion>(e))))
            .ForMember(dest => dest.Affiliations, opt => opt.MapFrom(
                src => src.Affiliations.Select(name => _affiliationsRepository.GetByNameAsync(name).Result)))
            .ForMember(dest => dest.Zones, opt => opt.MapFrom(
                src => src.Zones.Select(name => _zonesRepository.GetByNameAsync(name).Result)));
    }
}
