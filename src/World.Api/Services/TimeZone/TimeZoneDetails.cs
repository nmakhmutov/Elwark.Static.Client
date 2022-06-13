namespace World.Api.Services.TimeZone;

internal sealed record TimeZoneDetails(string Id, TimeSpan UtcOffset, string StandardName, string DisplayName);
