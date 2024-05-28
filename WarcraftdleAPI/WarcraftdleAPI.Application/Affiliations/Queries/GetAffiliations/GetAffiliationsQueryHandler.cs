using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WarcraftdleAPI.Application.Affiliations.Dtos;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliations;

public class GetAffiliationsQueryHandler(
    IAffiliationsRepository affiliationRepository,
    ILogger<GetAffiliationsQueryHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetAffiliationsQuery, IEnumerable<AffiliationDto>>
{
    public async Task<IEnumerable<AffiliationDto>> Handle(GetAffiliationsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all affiliations");

        var affiliations = await affiliationRepository.GetAsync();
        var affiliationDtos = mapper.Map<IEnumerable<AffiliationDto>>(affiliations);

        return affiliationDtos;
    }
}
