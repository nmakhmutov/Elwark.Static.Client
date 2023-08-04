using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using World.Api.Models;
using TimeZone = World.Api.Models.TimeZone;

namespace World.Api.Infrastructure;

internal sealed class WorldDbContextSeed
{
    private readonly IEnumerable<CultureInfo> _cultures;
    private readonly WorldDbContext _dbContext;

    public WorldDbContextSeed(WorldDbContext dbContext, IEnumerable<CultureInfo> cultures)
    {
        _dbContext = dbContext;
        _cultures = cultures;
    }

    public async Task SeedAsync()
    {
        await SeedCountriesAsync();
        await SeedTimeZonesAsync();
    }

    private async Task SeedCountriesAsync()
    {
        if (await _dbContext.Countries.AnyAsync())
            return;

        var client = new HttpClient(new SocketsHttpHandler());
        var options = new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        };

        var url = new StringBuilder("https://restcountries.com/v3.1/all?fields=")
            .Append("cca2,")
            .Append("cca3,")
            .Append("ccn3,")
            .Append("flags,")
            .Append("translations,")
            .Append("region,")
            .Append("subregion,")
            .Append("startOfWeek,")
            .Append("languages,")
            .Append("currencies,")
            .Append("name")
            .ToString();

        var countries = await client.GetFromJsonAsync<CountryDto[]>(url, options);

        if (countries is null)
            return;

        var continents = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Africa"] = "AF",
            ["North America"] = "NA",
            ["Oceania"] = "OC",
            ["Antarctic"] = "AN",
            ["Asia"] = "AS",
            ["Europe"] = "EU",
            ["South America"] = "SA"
        };

        foreach (var item in countries.OrderBy(x => x.Ccn3))
        {
            if (string.IsNullOrEmpty(item.Ccn3) || string.IsNullOrEmpty(item.Cca2) || string.IsNullOrEmpty(item.Cca3))
                continue;

            var subregion = string.IsNullOrWhiteSpace(item.Subregion) ? null : item.Subregion;
            var region = continents[GetRegion(item.Region, subregion)];
            var startOfWeek = Enum.TryParse<DayOfWeek>(item.StartOfWeek, true, out var result)
                ? result
                : DayOfWeek.Monday;
            
            var country = Country.Create(
                int.Parse(item.Ccn3),
                item.Cca2,
                item.Cca3,
                item.Flags["svg"],
                region,
                subregion,
                startOfWeek
            );

            foreach (var (language, _) in item.Languages)
                country.AddLanguage(new CultureInfo(language).TwoLetterISOLanguageName);

            foreach (var (currency, _) in item.Currencies)
                country.AddCurrency(currency);

            country.AddTranslation("en", item.Name.Common, item.Name.Official);

            foreach (var translation in item.Translations)
            {
                var culture = new CultureInfo(translation.Key);
                var language = culture.TwoLetterISOLanguageName;

                if (_cultures.Contains(culture))
                    country.AddTranslation(language, translation.Value.Common, translation.Value.Official);
            }

            await _dbContext.Countries.AddAsync(country);
        }

        await _dbContext.SaveChangesAsync();
    }

    private static string GetRegion(string region, string? subregion)
    {
        if (string.IsNullOrEmpty(subregion))
            return region;
        
        if (region != "Americas")
            return region;

        if (subregion is "Caribbean" or "Central America")
            return "North America";

        return subregion;
    }

    private async Task SeedTimeZonesAsync()
    {
        if (await _dbContext.TimeZones.AnyAsync())
            return;

        var result = new Dictionary<string, TimeZone>();
        foreach (var culture in _cultures)
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            TimeZoneInfo.ClearCachedData();

            foreach (var timeZoneInfo in TimeZoneInfo.GetSystemTimeZones().Where(x => x.HasIanaId))
            {
                var language = culture.TwoLetterISOLanguageName;

                if (!result.ContainsKey(timeZoneInfo.Id))
                    result.Add(timeZoneInfo.Id, new TimeZone(timeZoneInfo.Id, timeZoneInfo.BaseUtcOffset));

                result[timeZoneInfo.Id]
                    .AddTranslation(language, timeZoneInfo.StandardName, timeZoneInfo.DisplayName);
            }
        }

        await _dbContext.TimeZones.AddRangeAsync(result.Values);
        await _dbContext.SaveChangesAsync();
    }

    private sealed record CountryDto(
        string Ccn3,
        string Cca2,
        string Cca3,
        string Region,
        string? Subregion,
        string? StartOfWeek,
        NameDto Name,
        Dictionary<string, NameDto> Translations,
        Dictionary<string, Uri> Flags,
        Dictionary<string, string> Languages,
        Dictionary<string, JsonElement> Currencies
    );

    private sealed record NameDto(string Common, string Official);
}
