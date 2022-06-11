namespace World.Api.Endpoints;

internal static class TimeZoneEndpoints
{
    internal static IEndpointRouteBuilder MapTimeZoneEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/timezones", () =>
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones()
                .Where(x => x.HasIanaId)
                .OrderBy(x => x.BaseUtcOffset)
                .Select(x => new { x.Id, Name = x.DisplayName, Offset = x.BaseUtcOffset });

            return Results.Ok(timeZones);
        });

        return routes;
    }
}
