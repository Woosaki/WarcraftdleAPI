using MediatR;
using WarcraftdleAPI.Application.Characters.Dtos;

namespace WarcraftdleAPI.Application.Characters.Queries.GetCharacterById;

public class GetCharacterByIdQuery(int id) : IRequest<CharacterDto>
{
    public int Id { get; } = id;
}
