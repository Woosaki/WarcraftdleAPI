using AutoMapper;
using MediatR;
using WarcraftdleAPI.Application.Characters.Dtos;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Queries.GetCharacters;

public class GetCharactersQueryHandler(ICharactersRepository charactersRepository, IMapper mapper)
    : IRequestHandler<GetCharactersQuery, IEnumerable<CharacterDto>>
{
    public async Task<IEnumerable<CharacterDto>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await charactersRepository.GetAsync(request.StartsWith);
        var characterDtos = mapper.Map<IEnumerable<CharacterDto>>(characters);

        return characterDtos;
    }
}
