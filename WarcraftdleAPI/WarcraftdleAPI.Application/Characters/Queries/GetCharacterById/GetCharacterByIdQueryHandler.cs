using AutoMapper;
using MediatR;
using System.Net;
using WarcraftdleAPI.Application.Characters.Dtos;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Queries.GetCharacterById;

public class GetCharacterByIdQueryHandler(ICharactersRepository charactersRepository, IMapper mapper)
    : IRequestHandler<GetCharacterByIdQuery, CharacterDto>
{
    public async Task<CharacterDto> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
    {
        var character = await charactersRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Character with id {request.Id} could not be found", HttpStatusCode.NotFound);
        var characterDto = mapper.Map<CharacterDto>(character);

        return characterDto;
    }
}
