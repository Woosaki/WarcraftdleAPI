using MediatR;
using WarcraftdleAPI.Application.Affiliations.Dtos;

namespace WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliations;

public class GetAffiliationsQuery : IRequest<IEnumerable<AffiliationDto>>
{
}
