using MediatR;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Characters.Commands.DeleteCharacter;

public class DeleteCharacterCommandHandler(ICharactersRepository charactersRepository)
    : IRequestHandler<DeleteCharacterCommand>
{
    public async Task Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await charactersRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Character with id {request.Id} could not be found", HttpStatusCode.NotFound);

        await charactersRepository.DeleteAsync(character);
    }
}
