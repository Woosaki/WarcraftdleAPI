using AutoMapper;
using MediatR;
using WarcraftdleAPI.Domain.Repositories;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation;

public class CreateAffiliationCommandHandler(IAffiliationsRepository affiliationsRepository, IMapper mapper)
    : IRequestHandler<CreateAffiliationCommand, int>
{
    public async Task<int> Handle(CreateAffiliationCommand request, CancellationToken cancellationToken)
    {
        var affiliation = mapper.Map<Affiliation>(request);
        int id = await affiliationsRepository.CreateAsync(affiliation);

        return id;
    }
}
