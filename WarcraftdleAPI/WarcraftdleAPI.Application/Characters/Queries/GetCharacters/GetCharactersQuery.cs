using MediatR;
using WarcraftdleAPI.Application.Characters.Dtos;

namespace WarcraftdleAPI.Application.Characters.Queries.GetCharacters;

public class GetCharactersQuery(string? startsWith) : IRequest<IEnumerable<CharacterDto>>
{
    public string? StartsWith { get; } = startsWith;
}
