namespace WarcraftdleAPI.Application.Dtos.Affiliation;

public record AddMultipleAffiliationRequest
(
	IEnumerable<string> AffiliationNames
);
