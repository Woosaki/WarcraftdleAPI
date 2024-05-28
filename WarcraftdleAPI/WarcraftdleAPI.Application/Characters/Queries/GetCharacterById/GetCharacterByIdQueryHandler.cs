using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using WarcraftdleAPI.Application.Characters.Dtos;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Queries.GetCharacterById;

public class GetCharacterByIdQueryHandler(
    ICharactersRepository charactersRepository,
    ILogger<GetCharacterByIdQueryHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetCharacterByIdQuery, CharacterDto>
{
    public async Task<CharacterDto> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting character with ID: {Id}", request.Id);

        var character = await charactersRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Character with id {request.Id} could not be found", HttpStatusCode.NotFound);
        var characterDto = mapper.Map<CharacterDto>(character);

        return characterDto;
    }
}
