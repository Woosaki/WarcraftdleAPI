using AutoMapper;
using MediatR;
using System.Net;
using WarcraftdleAPI.Application.Affiliations.Dtos;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliationById;

public class GetAffiliationByIdQueryHandler(IAffiliationsRepository affiliationsRepository, IMapper mapper)
    : IRequestHandler<GetAffiliationByIdQuery, AffiliationDto>
{
    public async Task<AffiliationDto> Handle(GetAffiliationByIdQuery request, CancellationToken cancellationToken)
    {
        var affiliation = await affiliationsRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Affiliation with id {request.Id} could not be found", HttpStatusCode.NotFound);
        var affiliationDto = mapper.Map<AffiliationDto>(affiliation);

        return affiliationDto;
    }
}
