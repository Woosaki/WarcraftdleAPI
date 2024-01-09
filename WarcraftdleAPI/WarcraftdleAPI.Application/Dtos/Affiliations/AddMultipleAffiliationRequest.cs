namespace WarcraftdleAPI.Application.Dtos.Affiliations;

public record AddMultipleAffiliationRequest
(
	IEnumerable<string> AffiliationNames
);
