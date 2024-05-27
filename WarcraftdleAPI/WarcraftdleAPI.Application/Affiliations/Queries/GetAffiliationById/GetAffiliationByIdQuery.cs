using MediatR;
using WarcraftdleAPI.Application.Affiliations.Dtos;

namespace WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliationById;

public class GetAffiliationByIdQuery(int id) : IRequest<AffiliationDto>
{
    public int Id { get; } = id;
}
