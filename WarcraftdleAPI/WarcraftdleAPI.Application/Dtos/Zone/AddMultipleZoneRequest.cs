namespace WarcraftdleAPI.Application.Dtos.Zone;

public record AddMultipleZoneRequest
(
	IEnumerable<string> ZoneNames
);

