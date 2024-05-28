using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WarcraftdleAPI.Application.Characters.Dtos;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Queries.GetCharacters;

public class GetCharactersQueryHandler(
    ICharactersRepository charactersRepository,
    ILogger<GetCharactersQueryHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetCharactersQuery, IEnumerable<CharacterDto>>
{
    public async Task<IEnumerable<CharacterDto>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
    {
        var information = request.StartsWith == null ? "Getting all characters" : "Getting characters starting with {StartsWith}";
        logger.LogInformation(information, request.StartsWith);

        var characters = await charactersRepository.GetAsync(request.StartsWith);
        var characterDtos = mapper.Map<IEnumerable<CharacterDto>>(characters);

        return characterDtos;
    }
}
