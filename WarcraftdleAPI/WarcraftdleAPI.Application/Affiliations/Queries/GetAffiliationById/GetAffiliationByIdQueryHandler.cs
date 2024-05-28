using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using WarcraftdleAPI.Application.Affiliations.Dtos;
using WarcraftdleAPI.Domain.Exceptions;
using WarcraftdleAPI.Domain.Repositories;

namespace WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliationById;

public class GetAffiliationByIdQueryHandler(
    IAffiliationsRepository affiliationsRepository,
    ILogger<GetAffiliationByIdQueryHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetAffiliationByIdQuery, AffiliationDto>
{
    public async Task<AffiliationDto> Handle(GetAffiliationByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting affiliation with ID: {Id}", request.Id);

        var affiliation = await affiliationsRepository.GetByIdAsync(request.Id)
            ?? throw new ApiException($"Affiliation with id {request.Id} could not be found", HttpStatusCode.NotFound);
        var affiliationDto = mapper.Map<AffiliationDto>(affiliation);

        return affiliationDto;
    }
}
