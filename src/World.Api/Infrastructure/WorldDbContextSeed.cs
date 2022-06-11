using System.Globalization;
using Microsoft.EntityFrameworkCore;
using World.Api.Models;

namespace World.Api.Infrastructure;

internal sealed class WorldDbContextSeed
{
    private readonly WorldDbContext _dbContext;

    public WorldDbContextSeed(WorldDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Countries.AnyAsync())
            return;

        var client = new HttpClient();
        var countries = await client
            .GetFromJsonAsync<CountryDto[]>("https://restcountries.com/v3.1/all?fields=name,cca2,cca3,ccn3,flags,translations");

        if (countries is null)
            return;

        foreach (var item in countries.OrderBy(x => x.Ccn3))
        {
            if (string.IsNullOrEmpty(item.Ccn3) || string.IsNullOrEmpty(item.Cca2) || string.IsNullOrEmpty(item.Cca3))
                continue;

            var country = new Country(int.Parse(item.Ccn3), item.Cca2, item.Cca3, item.Flags["svg"]);
            country.AddTranslation("en", item.Name.Official, item.Name.Common);

            foreach (var translation in item.Translations)
            {
                var language = new CultureInfo(translation.Key).TwoLetterISOLanguageName;
                if (language.Length != 2)
                    continue;

                country.AddTranslation(language, translation.Value.Official, translation.Value.Common);
            }

            await _dbContext.Countries.AddAsync(country);
        }

        await _dbContext.SaveChangesAsync();
    }

    private sealed record CountryDto(
        string Ccn3,
        string Cca2,
        string Cca3,
        NameDto Name,
        Dictionary<string, NameDto> Translations,
        Dictionary<string, Uri> Flags
    );

    private sealed record NameDto(string Common, string Official);
}
