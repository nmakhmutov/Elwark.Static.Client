using System.Globalization;
using World.Api.Services.Country;

namespace World.Api.Endpoints;

internal static class CountryEndpoints
{
    internal static IEndpointRouteBuilder MapCountryEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/countries", async (CountryService service, CancellationToken ct) =>
        {
            var countries = new List<CountryOverview>();

            await foreach (var country in service.GetAsync(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, ct))
                countries.Add(country);

            return Results.Ok(countries);
        });

        routes.MapGet("/countries/{code}", async (string code, CountryService services, CancellationToken ct) =>
        {
            var country = await services
                .GetAsync(code.ToUpperInvariant(), CultureInfo.CurrentCulture.TwoLetterISOLanguageName, ct);

            return country is null ? Results.NotFound() : Results.Ok(country);
        });

        return routes;
    }
}
