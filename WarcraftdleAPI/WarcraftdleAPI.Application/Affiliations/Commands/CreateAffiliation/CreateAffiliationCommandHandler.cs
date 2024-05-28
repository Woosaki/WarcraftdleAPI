using AutoMapper;
using MediatR;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation;

public class CreateAffiliationCommandHandler(
    IAffiliationsRepository affiliationsRepository,
    ILogger<CreateAffiliationCommandHandler> logger,
    IMapper mapper)
    : IRequestHandler<CreateAffiliationCommand, int>
{
    public async Task<int> Handle(CreateAffiliationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new affiliation {Affiliation}", request.Name);

        var affiliation = mapper.Map<Affiliation>(request);
        int id = await affiliationsRepository.CreateAsync(affiliation);

        return id;
    }
}
