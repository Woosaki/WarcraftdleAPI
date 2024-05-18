using AutoMapper;
using MediatR;
using WarcraftdleAPI.Application.Affiliations.Dtos;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliations;

public class GetAffiliationsQueryHandler(IAffiliationsRepository affiliationRepository, IMapper mapper)
    : IRequestHandler<GetAffiliationsQuery, IEnumerable<AffiliationDto>>
{
    public async Task<IEnumerable<AffiliationDto>> Handle(GetAffiliationsQuery request, CancellationToken cancellationToken)
    {
        var affiliations = await affiliationRepository.GetAllAsync();
        var affiliationDtos = mapper.Map<IEnumerable<AffiliationDto>>(affiliations);

        return affiliationDtos;
    }
}
