namespace WarcraftdleAPI.Application.Dtos.Zones;

public record AddMultipleZoneRequest
(
    IEnumerable<string> ZoneNames
);