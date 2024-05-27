using MediatR;

namespace WarcraftdleAPI.Application.Characters.Commands.DeleteCharacter;

public class DeleteCharacterCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
