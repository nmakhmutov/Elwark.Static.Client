using System.Globalization;
using World.Api.Services.Country;

namespace World.Api.Endpoints;

internal static class CountryEndpoints
{
    internal static IEndpointRouteBuilder MapCountryEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/countries", (CountryService service, CancellationToken ct) =>
                Results.Ok(service.GetAsync(CultureInfo.CurrentCulture, ct))
            )
            .CacheOutput(policy => policy
                .Expire(TimeSpan.FromDays(1))
                .VaryByValue($"{nameof(CountryEndpoints)}-root", CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            );

        routes.MapGet("/countries/{code}", async (string code, CountryService services, CancellationToken ct) =>
        {
            var country = await services.GetAsync(code.ToUpperInvariant(), CultureInfo.CurrentCulture, ct);

            return country is null ? Results.NotFound() : Results.Ok(country);
        });

        return routes;
    }
}
