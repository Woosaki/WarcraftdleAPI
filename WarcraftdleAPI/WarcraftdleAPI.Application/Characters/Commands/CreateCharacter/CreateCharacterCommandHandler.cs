using AutoMapper;
using MediatR;
using WarcraftdleAPI.Domain.Entities;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommandHandler(
    ICharactersRepository charactersRepository,
    IAffiliationsRepository affiliationsRepository,
    IZonesRepository zonesRepository,
    IMapper mapper)
    : IRequestHandler<CreateCharacterCommand, int>
{
    public async Task<int> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = mapper.Map<Character>(request);
        character.Affiliations = await affiliationsRepository.GetAsync(request.Affiliations);
        character.Zones = await zonesRepository.GetAsync(request.Zones);
        int id = await charactersRepository.CreateAsync(character);

        return id;
    }
}
