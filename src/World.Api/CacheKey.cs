using World.Api.Models;

namespace World.Api;

internal static class CacheKey
{
    public static string CountryLanguages() =>
        "world-api-countries-languages";

    public static string Countries(Language language) =>
        $"world-api-countries-{language}";

    public static string TimeZones(Language language) =>
        $"world-api-time-zones-{language}";
}
